
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.OperationModule.Commands.DeleteOperation
{
   public record DeleteOperationCommand : IRequest<CustomResponse>
    {
        public int Id { get; set; } 
    }
    public class DeleteOperationCommandHandler : IRequestHandler<DeleteOperationCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public DeleteOperationCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(DeleteOperationCommand request, CancellationToken cancellationToken)
        {
          var found_Operation = await _context.Operations.FindAsync(request.Id);
        if(found_Operation == null){
            throw new GhionException(CustomResponse.NotFound($"Operation with id = {request.Id} is not found"));
        }
        _context.Operations.Remove(found_Operation);
            await _context.SaveChangesAsync(cancellationToken);

         return CustomResponse.Succeeded("Operation deleted successfully!");
        }
    }
}