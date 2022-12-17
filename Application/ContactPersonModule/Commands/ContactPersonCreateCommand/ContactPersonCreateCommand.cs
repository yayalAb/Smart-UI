using Domain.Entities;
using MediatR;

namespace Application.ContactPersonModule.Commands.ContactPersonCreateCommand
{
    public record ContactPersonCreateCommand : IRequest<ContactPerson>
    {
        public string Name { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string Phone { get; init; } = null!;
        public string TinNumber { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}