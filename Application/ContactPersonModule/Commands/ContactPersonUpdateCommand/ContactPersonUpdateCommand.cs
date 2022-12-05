using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;

namespace Application.ContactPersonModule.Commands.ContactPersonUpdateCommand
{

    public class ContactPersonUpdateCommand : IRequest<ContactPerson>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string? Phone { get; set; }
        public string TinNumber { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
    }

}