
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;

namespace Application.OperationFollowupModule.Commands.UpdateStatus;

public record UpdateStatus : IRequest<CustomResponse> {
    public string GeneratedDocumentName { get; init; }
    public DateTime GeneratedDate { get; init; }
    public bool IsApproved { get; init; } = false;
    public DateTime? ApprovedDate { get; init; } = null!;
    public int Id { get; init; }
}

public class UpdateStatusHandler : IRequestHandler<UpdateStatus, CustomResponse> {

    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateStatusHandler(IAppDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomResponse> Handle(UpdateStatus request, CancellationToken cancellationToken) {
        
        var status = await _context.OperationStatuses.FindAsync(request.Id);
        
        if(status == null){
            throw new GhionException(CustomResponse.NotFound("Operation status not found"));
        }

        status.GeneratedDocumentName = request.GeneratedDocumentName;
        status.GeneratedDate = request.GeneratedDate;
        status.IsApproved = request.IsApproved;
        status.ApprovedDate = request.ApprovedDate;

        await _context.SaveChangesAsync(cancellationToken);
        return CustomResponse.Succeeded("Operation status updated!");

    }

}