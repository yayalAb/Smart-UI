
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommand : IRequest<int>
    {
        public int Id { get; set; } 
        public string? Email { get; init; }
        public string? Phone { get; init; }
        public string? Region { get; init; }
        public string? City { get; init; }
        public string? Subcity { get; init; }
        public string? Country { get; init; }
        public string? POBOX { get; init; }
    }
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, int>
    {
        private readonly IAppDbContext _context;

        public UpdateAddressCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var existingAddress = await _context.Addresses.FindAsync(request.Id);
            if (existingAddress == null)
            {
                throw new NotFoundException("Adress", new { Id = request.Id });

            };
           
            existingAddress.Email = request.Email;  
            existingAddress.Phone = request.Phone;
            existingAddress.Region = request.Region;    
            existingAddress.City = request.City;    
            existingAddress.Subcity = request.Subcity;  
            existingAddress.Country = request.Country;  
            existingAddress.POBOX = request.POBOX;  

            _context.Addresses.Update(existingAddress);
            await _context.SaveChangesAsync(cancellationToken);
            return existingAddress.Id;
        }
    }
}