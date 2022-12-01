
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationFollowupModule;

public class OperationEventHandler
{

    IAppDbContext _context;

    public OperationEventHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> DocumentGenerationEventAsync(CancellationToken cancellationToken, OperationStatus status, string statusName)
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
                        if (operation == null)
                        {
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

    public async Task ApproveDocumentEventAsync(string documentName, int operationId, CancellationToken cancellationToken)
    {

        var operation = await _context.Operations.FindAsync(operationId);

        if (operation == null)
        {
            throw new GhionException(CustomResponse.NotFound("Operation doesn't exist!"));
        }

        if (documentName == Enum.GetName(typeof(Documents), Documents.Number4))
        {
            operation.Status = Enum.GetName(typeof(Status), Status.Number4Approved);
        }
        else if (documentName == Enum.GetName(typeof(Documents), Documents.ImportNumber9))
        {
            operation.Status = Enum.GetName(typeof(Status), Status.ImportNumber9Approved);
        }
        else if (documentName == Enum.GetName(typeof(Documents), Documents.TransferNumber9))
        {
            operation.Status = Enum.GetName(typeof(Status), Status.Closed);
        }
        else if (documentName == Enum.GetName(typeof(Documents), Documents.Number1))
        {
            operation.Status = Enum.GetName(typeof(Status), Status.Closed);
        }

        await _context.SaveChangesAsync(cancellationToken);

    }

    public async Task DisapproveDocumentEventAsync(string documentName, int operationId, CancellationToken cancellationToken)
    {

        var operation = await _context.Operations.FindAsync(operationId);

        if (operation == null)
        {
            throw new GhionException(CustomResponse.NotFound("Operation doesn't exist!"));
        }

        if (documentName == Enum.GetName(typeof(Documents), Documents.Number4))
        {
            operation.Status = Enum.GetName(typeof(Status), Status.Number4Generated);
        }
        else if (documentName == Enum.GetName(typeof(Documents), Documents.ImportNumber9))
        {
            operation.Status = Enum.GetName(typeof(Status), Status.ImportNumber9Generated);
        }
        else if (documentName == Enum.GetName(typeof(Documents), Documents.TransferNumber9))
        {
            operation.Status = Enum.GetName(typeof(Status), Status.TransferNumber9Generated);
        }
        else if (documentName == Enum.GetName(typeof(Documents), Documents.Number1))
        {
            operation.Status = Enum.GetName(typeof(Status), Status.Number1Generated);
        }

        await _context.SaveChangesAsync(cancellationToken);

    }

    public async Task<bool> IsDocumentGenerated(int operationId, string documentName)
    {
        return await _context.OperationStatuses
            .Where(os => os.OperationId == operationId && os.GeneratedDocumentName == documentName).AnyAsync();
    }
    public async Task<bool> IsDocumentApproved(int operationId, string documentName)
    {
        return await _context.OperationStatuses
            .Where(os => os.OperationId == operationId && os.GeneratedDocumentName == documentName && os.IsApproved).AnyAsync();
    }
}