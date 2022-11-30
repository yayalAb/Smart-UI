
using System.Reflection.Metadata;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
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

        public DsipatchECDCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(DsipatchECDCommand request, CancellationToken cancellationToken)
        {
            var found_Operation = await _context.Operations.FindAsync(request.OperationId);
            if (found_Operation == null)
            {
                throw new GhionException(CustomResponse.NotFound($"Operation with id = {request.OperationId} is not found"));
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