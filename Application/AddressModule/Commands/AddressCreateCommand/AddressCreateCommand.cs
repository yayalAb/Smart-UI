using Domain.Entities;
using MediatR;

namespace Application.AddressModule.Commands.AddressCreateCommand {

    public record AddressCreateCommand : IRequest<Address> {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Subcity { get; set; }
        public string Country { get; set; }
        public string? POBOX { get; set; }
    }

}