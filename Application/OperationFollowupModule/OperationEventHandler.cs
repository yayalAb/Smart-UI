
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.OperationFollowupModule;

public class OperationEventHandler {

    IAppDbContext _context;

    public OperationEventHandler(IAppDbContext context){
        _context = context;
    }

    public async void DocumentGenerationEventAsync(CancellationToken cancellationToken, OperationStatus status) {

        var statuses = _context.OperationStatuses.Where(o => o.OperationId == status.OperationId && o.GeneratedDocumentName == status.GeneratedDocumentName).Any();

        if(!statuses){
            _context.OperationStatuses.Add(status);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }

    public async void ApproveDocumentAsync(CancellationToken cancellationToken, int statusId) {
        throw new NotImplementedException();
    }
    
}