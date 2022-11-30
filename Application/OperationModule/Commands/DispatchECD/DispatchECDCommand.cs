
using System.Reflection.Metadata;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using Domain.Enums;
using MediatR;

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

            found_Operation.Status = Enum.GetName(typeof(Status), Status.ECDDispatched)!;
            _context.Operations.Update(found_Operation);
            await _context.SaveChangesAsync(cancellationToken);

            return CustomResponse.Succeeded("ECD document dispatched  successfully!");
        }
    }
}