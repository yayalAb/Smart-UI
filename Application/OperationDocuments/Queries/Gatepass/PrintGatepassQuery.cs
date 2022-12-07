
using MediatR;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Application.OperationFollowupModule;
using Application.Common.Exceptions;

namespace Application.OperationDocuments.Queries.Gatepass;

public record PrintGatepassQuery : IRequest<GatepassDto>
{
    public int TruckAssignmentId { get; set; }
}
public class PrintGatepassQueryHandler : IRequestHandler<PrintGatepassQuery, GatepassDto>
{

    private readonly IAppDbContext _context;
    private readonly ILogger logger;
    private readonly OperationEventHandler _operationEventHandler;
    public PrintGatepassQueryHandler(IAppDbContext context, ILogger<PrintGatepassQuery> logger, OperationEventHandler operationEventHandler)
    {
        _context = context;
        this.logger = logger;
        _operationEventHandler = operationEventHandler;
    }

    public async Task<GatepassDto> Handle(PrintGatepassQuery request, CancellationToken cancellationToken)
    {

        if (!await _context.TruckAssignments.AnyAsync(o => o.Id == request.TruckAssignmentId))
        {
            throw new GhionException(CustomResponse.NotFound("There is no TruckAssignment with the given Id!"));
        }



        //fetch gatepass form data 
        var data = _context.TruckAssignments
           .Where(ta => ta.Id == request.TruckAssignmentId)
           .Include(ta => ta.Containers)!
               .ThenInclude(c => c.Goods)
           .Include(ta => ta.Truck)
           .Include(ta => ta.Goods)
           .Include(ta => ta.Operation)
           .Select(ta => new
           {
               Date = ta.Created,
               OperationNumber = ta.Operation.OperationNumber,
               TruckNumber = ta.Truck.TruckNumber,
               Containers = ta.Containers,
               Goods = ta.Goods
           }).First();

        int Quantity = 0;
        float weight = 0;
        List<string> descriptions = new List<string>();
        List<string> containerNumbers = new List<string>();
        if (data.Containers != null)
        {

            Quantity += data.Containers
                .SelectMany(c => c.Goods.Select(g => g.NumberOfPackages)).Sum();
            weight += data.Containers.SelectMany(c => c.Goods.Select(g => g.Weight)).Sum();
            descriptions
                .AddRange(data.Containers
                    .SelectMany(c => c.Goods.Select(g => g.Description)));


            containerNumbers.AddRange(data.Containers.Select(c => c.ContianerNumber));
        }
        if (data.Goods != null)
        {
            Quantity += data.Goods
                .Select(g => g.NumberOfPackages).Sum();
            weight += data.Goods.Select(g => g.Weight).Sum();
            descriptions
                .AddRange(data.Goods
                   .Select(g => g.Description));
        }
        return new GatepassDto
        {
            Date = data.Date,
            OperationNumber = data.OperationNumber,
            TruckNumber = data.TruckNumber,
            Quantity = Quantity,
            Weight = weight,
            Descriptions = descriptions,
            ContainerNumbers = containerNumbers,
            Localization = ""
        };

    }



}