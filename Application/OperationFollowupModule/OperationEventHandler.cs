
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationFollowupModule;

public class OperationEventHandler
{

    IAppDbContext _context;

    public OperationEventHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> DocumentGenerationEventAsync(CancellationToken cancellationToken, OperationStatus status , string statusName)
    {
        var executionStrategy = _context.database.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(async () =>
        {
            using (var transaction = _context.database.BeginTransaction())
            {
                try
                {
                    var statuses = _context.OperationStatuses.Where(o => o.OperationId == status.OperationId && o.GeneratedDocumentName == status.GeneratedDocumentName).Any();

                    if (!statuses)
                    {
                        // add operation status for the operation
                        _context.OperationStatuses.Add(status);
                        await _context.SaveChangesAsync(cancellationToken);

                        var operation = await _context.Operations.FindAsync(status.OperationId);
                        if(operation == null){
                            throw new GhionException(CustomResponse.NotFound($"operation with id {status.OperationId} is not found while generating document "));
                        }
                        // change status of the operation
                        operation.Status = statusName;
                        _context.Operations.Update(operation);
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);

                    }

                    return true; 
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
        });


    }

    public async void ApproveDocumentAsync(CancellationToken cancellationToken, int statusId)
    {
        throw new NotImplementedException();
    }

}