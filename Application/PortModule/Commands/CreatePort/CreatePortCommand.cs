
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.PortModule.Commands.CreatePort
{
    public record CreatePortCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string? Country { get; set; }
        public string? Region { get; set; }
        public string? Vollume { get; set; }
    }
    public class CreatePortCommandHandler : IRequestHandler<CreatePortCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreatePortCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreatePortCommand request, CancellationToken cancellationToken)
        {
            Port newPort = new Port()
            {
                Name = request.Name,    
                Country = request.Country,
                Region = request.Region,
                Vollume = request.Vollume,
            };

            await _context.Ports.AddAsync(newPort);
            await _context.SaveChangesAsync(cancellationToken);
            return newPort.Id;
        }
    }

}
