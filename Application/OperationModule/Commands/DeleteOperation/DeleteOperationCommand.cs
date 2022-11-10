
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.OperationModule.Commands.DeleteOperation
{
   public record DeleteOperationCommand : IRequest<bool>
    {
        public int Id { get; set; } 
    }
    public class DeleteOperationCommandHandler : IRequestHandler<DeleteOperationCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeleteOperationCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteOperationCommand request, CancellationToken cancellationToken)
        {
            var existingOperation = await _context.Operations.FindAsync(request.Id);
            if (existingOperation == null)
            {
                throw new NotFoundException("Operation", new { Id = request.Id });

            };

            _context.Operations.Remove(existingOperation);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}