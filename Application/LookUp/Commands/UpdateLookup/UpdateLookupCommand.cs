﻿
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.LookUp.Commands.UpdateLookup
{
    public class UpdateLookupCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Key { get; init; }
        public string Value { get; init; }
    }
    public class UpdateLookupCommandHandler : IRequestHandler<UpdateLookupCommand, int>
    {
        private readonly IAppDbContext _context;

        public UpdateLookupCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateLookupCommand request, CancellationToken cancellationToken)
        {
            var existingLookup = await _context.Lookups.FindAsync(request.Id);
            if(existingLookup == null)
            {
                throw new NotFoundException("Lookup", new { Id = request.Id });

            };
            existingLookup.Key = request.Key; 
            existingLookup.Value = request.Value; 
            _context.Lookups.Update(existingLookup);
            await _context.SaveChangesAsync(cancellationToken);
            return existingLookup.Id;

        }
    }
}
