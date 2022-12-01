using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.ContactPersonModule.Commands.ContactPersonCreateCommand
{
    public record ContactPersonCreateCommand : IRequest<ContactPerson> {
        public string Name { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string Phone { get; init; } = null!;
    }
}