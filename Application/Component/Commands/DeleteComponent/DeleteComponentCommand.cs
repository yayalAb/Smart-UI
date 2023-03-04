
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Component.Commands.DeleteComponent
{
    public record DeleteComponentCommand : IRequest<CustomResponse>
    {
        public int Id { get; init; }
    }
    public class DeleteComponentCommandHandler : IRequestHandler<DeleteComponentCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public DeleteComponentCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(DeleteComponentCommand request, CancellationToken cancellationToken)
        {
            var Componet = await _context.Components.FindAsync(request.Id);
            if (Componet == null)
            {
                throw new GhionException(CustomResponse.NotFound($"Componet with id = {request.Id} is not found"));
            }
            _context.Components.Remove(Componet);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Componet deleted successfully!");
        }
    }
}
