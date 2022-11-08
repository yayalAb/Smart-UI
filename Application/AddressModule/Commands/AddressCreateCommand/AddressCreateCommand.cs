using Domain.Entities;
using MediatR;

namespace Application.AddressModule.Commands.AddressCreateCommand {

    public record AddressCreateCommand : IRequest<Address> {
        public string Email { get; init; }
        public string Phone { get; init; }
        public string Region { get; init; }
        public string City { get; init; }
        public string Subcity { get; init; }
        public string Country { get; init; }
        public string POBOX { get; init; }
    }

}