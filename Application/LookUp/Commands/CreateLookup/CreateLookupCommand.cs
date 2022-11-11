

using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.LookUp.Commands.CreateLookup
{
    public record CreateLookupCommand : IRequest<int>
    {
        public string Name { get; init; }
        public string Type { get; init; }   
    }
    public class CreateLookupCommandHandler : IRequestHandler<CreateLookupCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateLookupCommandHandler(IAppDbContext context)
        {
           _context = context;
        }
        public async Task<int> Handle(CreateLookupCommand request, CancellationToken cancellationToken)
        {
            Lookup newLookup = new Lookup
            {
                Name = request.Name,
                Type = request.Type,
            };

            await _context.Lookups.AddAsync(newLookup);
            await _context.SaveChangesAsync(cancellationToken);
            return newLookup.Id;
           
        }
    }
}
