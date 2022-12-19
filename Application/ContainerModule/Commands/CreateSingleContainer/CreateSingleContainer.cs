
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.ContainerModule.Commands.CreateSingleContainer;

public record CreateSingleContainer : IRequest<CustomResponse>
{
    public string ContianerNumber { get; set; } = null!;
    public string? GoodsDescription { get; set; }
    public string SealNumber { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Size { get; set; }
    public string WeightMeasurement { get; set; }
    public string Currency { get; set; }
    public int LocationPortId { get; set; }
    public int OperationId { get; set; }
}

public class CreateSingleContainerHandler : IRequestHandler<CreateSingleContainer, CustomResponse>
{

    private readonly IAppDbContext _context;
    private readonly IFileUploadService _fileUploadService;
    private readonly IMapper _mapper;

    public CreateSingleContainerHandler(IAppDbContext context, IFileUploadService fileUploadService, IMapper mapper)
    {
        _context = context;
        _fileUploadService = fileUploadService;
        _mapper = mapper;
    }

    public async Task<CustomResponse> Handle(CreateSingleContainer request, CancellationToken cancellationToken)
    {

        var operation = await _context.Operations.FindAsync(request.OperationId);

        if (operation == null)
        {
            throw new GhionException(CustomResponse.NotFound("operation not found"));
        }

        Container newContainer = _mapper.Map<Container>(request);
        _context.Containers.Add(newContainer);
        await _context.SaveChangesAsync(cancellationToken);

        return CustomResponse.Succeeded("Container Created Successfully!");

    }
}