
using System.Reflection.Metadata;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationModule.Commands.DispatchECD
{
    public record DsipatchECDCommand : IRequest<CustomResponse>
    {
        public int OperationId { get; set; }
        public string? ECDDocument { get; set; }

    }
    public class DsipatchECDCommandHandler : IRequestHandler<DsipatchECDCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;
        private readonly OperationEventHandler _operationEventHandler;

        public DsipatchECDCommandHandler(IAppDbContext context, OperationEventHandler operationEventHandler)
        {
            _context = context;
            _operationEventHandler = operationEventHandler;
        }
        public async Task<CustomResponse> Handle(DsipatchECDCommand request, CancellationToken cancellationToken)
        {
            var executionStrategy = _context.database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.database.BeginTransaction())
                {
                    try
                    {
                        var found_Operation = await _context.Operations.FindAsync(request.OperationId);
                        if (found_Operation == null)
                        {
                            throw new GhionException(CustomResponse.NotFound($"Operation with id = {request.OperationId} is not found"));
                        }

                        //checking preconditions for dispatching ecd doc
                        if (!await _operationEventHandler.IsDocumentGenerated(found_Operation.Id, Enum.GetName(typeof(Documents), Documents.GoodsRemoval)!))
                        {
                            throw new GhionException(CustomResponse.BadRequest($"Goods removal document must be generated before dispatching ECD documnet"));
                        }

                        if (request.ECDDocument != null)
                        {
                            found_Operation.ECDDocument = request.ECDDocument;
                        }
                        // update operation status and generate doc
                        var statusName = Enum.GetName(typeof(Status), Status.ECDDispatched);
                        await _operationEventHandler.DocumentGenerationEventAsync(cancellationToken, new OperationStatus
                        {
                            GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.ECDDocument)!,
                            GeneratedDate = DateTime.Now,
                            IsApproved = true,
                            OperationId = request.OperationId
                        }, statusName!);

                        _context.Operations.Update(found_Operation);
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync();
                        return CustomResponse.Succeeded("ECD document dispatched  successfully!");
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            });

        }
    }
}