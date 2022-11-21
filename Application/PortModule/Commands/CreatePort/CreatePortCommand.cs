
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.PortModule.Commands.CreatePort
{
    public record CreatePortCommand : IRequest<CustomResponse>
    {
        public string PortNumber { get; set; }
        public string? Country { get; set; }
        public string? Region { get; set; }
        public string? Vollume { get; set; }
    }
    public class CreatePortCommandHandler : IRequestHandler<CreatePortCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public CreatePortCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(CreatePortCommand request, CancellationToken cancellationToken)
        {
            Port newPort = new Port()
            {
                PortNumber = request.PortNumber,    
                Country = request.Country,
                Region = request.Region,
                Vollume = request.Vollume,
            };

            await _context.Ports.AddAsync(newPort);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Port Created!");
        }
    }

}
