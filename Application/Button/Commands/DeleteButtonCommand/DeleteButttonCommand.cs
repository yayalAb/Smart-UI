using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Button.Commands.DeleteButtonCommand
{
    public record DeleteButttonCommand : IRequest<CustomResponse>
    {
        public int Id { get; init; }
    }
    public class DeleteButttonCommandHandler : IRequestHandler<DeleteButttonCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public DeleteButttonCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(DeleteButttonCommand request, CancellationToken cancellationToken)
        {
            var Button = await _context.buttons.FindAsync(request.Id);
            if (Button == null)
            {
                throw new GhionException(CustomResponse.NotFound($"Button with id = {request.Id} is not found"));
            }
            _context.buttons.Remove(Button);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Button deleted successfully!");
        }
    }
}
