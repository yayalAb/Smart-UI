

using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.LookUp.Commands.CreateLookup
{
    public record CreateLookupCommand : IRequest<CustomResponse>
    {
        public string Key { get; init; }
        public string Value { get; init; }
        public byte? IsParent { get; init; } = 0!;
    }
    public class CreateLookupCommandHandler : IRequestHandler<CreateLookupCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public CreateLookupCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(CreateLookupCommand request, CancellationToken cancellationToken)
        {
            Lookup newLookup = new Lookup
            {
                Key = request.Key,
                Value = request.Value,
                IsParent = request.IsParent ?? 0
            };

            _context.Lookups.Add(newLookup);

            if (request.IsParent == 1)
            {
                _context.Lookups.Add(new Lookup()
                {
                    Key = "key",
                    Value = request.Value,
                    IsParent = 1
                });
            }

            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Lookup created!");

        }
    }
}
