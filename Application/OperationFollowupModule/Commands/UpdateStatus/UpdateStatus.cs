
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;

namespace Application.OperationFollowupModule.Commands.UpdateStatus;

public record UpdateStatus : IRequest<CustomResponse>{
    public string GeneratedDocumentName { get; set; }
    public DateTime GeneratedDate { get; set; }
    public bool IsApproved { get; set; } = false;
    public DateTime? ApprovedDate { get; set; }
    public int OperationId { get; set; }
}

public class UpdateStatusHandler : IRequestHandler<UpdateStatus, CustomResponse> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateStatusHandler(IAppDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<CustomResponse> Handle(UpdateStatus request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
