
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.LookUp.Commands.UpdateLookup
{
    public class UpdateLookupCommand : IRequest<CustomResponse>
    {
        public int Id { get; set; }
        public string Key { get; init; }
        public string Value { get; init; }
    }
    public class UpdateLookupCommandHandler : IRequestHandler<UpdateLookupCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public UpdateLookupCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(UpdateLookupCommand request, CancellationToken cancellationToken)
        {
            
            var existingLookup = await _context.Lookups.FindAsync(request.Id);
            if(existingLookup == null)
            {
                throw new GhionException(CustomResponse.NotFound("Lookup not found!"));
            };
            existingLookup.Key = request.Key; 
            existingLookup.Value = request.Value; 
            _context.Lookups.Update(existingLookup);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Lookup Updated Successfully!");

        }
    }
}
