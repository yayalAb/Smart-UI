

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.LookUp.Commands.DeleteLookup
{
    public record DeleteLookupCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteLookupCommandHandler : IRequestHandler<DeleteLookupCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeleteLookupCommandHandler(IAppDbContext context)
        {
           _context = context;
        }
        public async  Task<bool> Handle(DeleteLookupCommand request, CancellationToken cancellationToken)
        {
            var existingLookup = await _context.Lookups.FindAsync(request.Id);
            if (existingLookup == null)
            {
                throw new NotFoundException("Lookup", new { Id = request.Id });

            };
           
            _context.Lookups.Remove(existingLookup);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
