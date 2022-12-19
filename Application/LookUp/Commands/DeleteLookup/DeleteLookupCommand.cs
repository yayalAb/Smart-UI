using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.LookUp.Commands.DeleteLookup
{
    public record DeleteLookupCommand : IRequest<CustomResponse>
    {
        public List<int> Ids { get; set; } = new List<int>()!;
    }
    public class DeleteLookupCommandHandler : IRequestHandler<DeleteLookupCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public DeleteLookupCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(DeleteLookupCommand request, CancellationToken cancellationToken)
        {
            var existingLookup = _context.Lookups.Where(l => request.Ids.Contains(l.Id)).ToList();
            _context.Lookups.RemoveRange(existingLookup);
            // if (existingLookup == null)
            // {
            //     throw new NotFoundException("Lookup", new { Id = request.Id });

            // };

            // _context.Lookups.Remove(existingLookup);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Lookups deleted successfully!");

        }
    }
}
