using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.ContactPersonModule.Commands.ContactPersonCreateCommand
{
    public record ContactPersonCreateCommand : IRequest<ContactPerson> {
        public string Name { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
    }
}