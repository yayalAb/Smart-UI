
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.GoodModule;
using Application.GoodModule.Queries;
using Application.OperationFollowupModule;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.Number1;

public record GenerateNumber1Query : IRequest<Number1Dto>
{
    public int OperationId { get; init; }
}

public class GenerateNumber1QueryHandler : IRequestHandler<GenerateNumber1Query, Number1Dto>
{

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly OperationEventHandler _operationEvent;

    public GenerateNumber1QueryHandler(IAppDbContext context, IMapper mapper, OperationEventHandler operationEvent)
    {
        _context = context;
        _mapper = mapper;
        _operationEvent = operationEvent;
    }

    public async Task<Number1Dto> Handle(GenerateNumber1Query request, CancellationToken cancellationToken)
    {
        if (!await _context.Operations.AnyAsync(o => o.Id == request.OperationId))
        {
            throw new GhionException(CustomResponse.NotFound("There is no Operation with the given Id!"));
        }
        var date = DateTime.Now;
        // fetch number1 form data
        Number1Dto data =  _context.Operations
            .Include(o => o.Company)
            .Include(o => o.Payments.Where(p => p.Name == "DO"))
            .Include(o => o.Goods)
            .Where(o => o.Id == request.OperationId )
            .Select(o => new Number1Dto
            {
                Date = date,
                CodeNIF = o.Company.CodeNIF,
                SNumber = o.SNumber,
                SDate = o.SDate,
                DONumber = o.Payments == null || o.Payments.ToList().Count == 0
                                    ? null
                                    : o.Payments.First().DONumber,
                DODate = o.Payments == null || o.Payments.ToList().Count == 0
                                    ? null
                                    : o.Payments.First().PaymentDate,
                TotalNumberOfPackages = o.Goods == null || o.Goods.Count == 0
                                    ? null
                                    : o.Goods.Select(g => g.NumberOfPackages).Sum(),
                RecepientName = o.RecepientName,
                VesselName = o.VesselName,
                ArrivalDate = o.ArrivalDate,
                VoyageNumber = o.VoyageNumber,
                ConnaissementNumber = o.ConnaissementNumber,
                CountryOfOrigin = o.CountryOfOrigin,
                REGTax = o.REGTax,
                Goods = _mapper.Map<ICollection<DocGoodDto>>(o.Goods),
                // TODO: find out source of ports data
                SourceLocation = null,
                DestinationLocation = null
            }).First();

        // update operation status and generate doc
        var statusName = Enum.GetName(typeof(Status), Status.Number1Generated);
        await _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus
        {
            GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.Number1)!,
            GeneratedDate = date,
            IsApproved = false,
            OperationId = request.OperationId
        },
         statusName!
         );

        return data;

    }
}