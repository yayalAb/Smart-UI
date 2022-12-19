
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.OperationFollowupModule.Commands.UpdateStatus;

public record UpdateStatus : IRequest<CustomResponse>
{
    public int Id { get; init; }
}

public class UpdateStatusHandler : IRequestHandler<UpdateStatus, CustomResponse>
{

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;

    public UpdateStatusHandler(IAppDbContext context, OperationEventHandler operationEvent)
    {
        _context = context;
        _operationEvent = operationEvent;
    }

    public async Task<CustomResponse> Handle(UpdateStatus request, CancellationToken cancellationToken)
    {

        var status = await _context.OperationStatuses.FindAsync(request.Id);

        if (status == null)
        {
            throw new GhionException(CustomResponse.NotFound("Operation status not found"));
        }

        if (status.IsApproved == true)
        {
            status.IsApproved = false;
            status.ApprovedDate = null;
            await _operationEvent.DisapproveDocumentEventAsync(status.GeneratedDocumentName, status.OperationId, cancellationToken);
            return CustomResponse.Succeeded("Document has already been approved!");
        }

        status.IsApproved = true;
        status.ApprovedDate = DateTime.Now;
        await _operationEvent.ApproveDocumentEventAsync(status.GeneratedDocumentName, status.OperationId, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return CustomResponse.Succeeded("Document approved!");

    }

}