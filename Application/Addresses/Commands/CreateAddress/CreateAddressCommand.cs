

using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Addresses.Commands.CreateAddress
{
    public record  CreateAddressCommand : IRequest<int>
    {
        public string? Email { get; init; }
        public string? Phone { get; init; }
        public string? Region { get; init; }
        public string? City { get; init; }
        public string? Subcity { get; init; }
        public string? Country { get; init; }   
        public string? POBOX { get; init; } 
    }
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateAddressCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            Address newAddress = new Address
            {
                Email = request.Email,
                Phone = request.Phone,
                Region = request.Region,    
                City = request.City,    
                Subcity = request.Subcity,
                Country = request.Country,
                POBOX = request.POBOX,  
            };

            await _context.Addresses.AddAsync(newAddress);
            await _context.SaveChangesAsync(cancellationToken);
            return newAddress.Id;

        }
    }
}