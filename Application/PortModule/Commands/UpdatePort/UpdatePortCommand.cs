

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.PortModule.Commands.UpdatePort
{
    public record UpdatePortCommand : IRequest<CustomResponse>
    {
        public int Id { get; set; }
        public string PortNumber { get; set; }
        public string? Country { get; set; }
        public string? Region { get; set; }
        public string? Vollume { get; set; }
    }
    public class UpdatePortCommandHandler : IRequestHandler<UpdatePortCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public UpdatePortCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(UpdatePortCommand request, CancellationToken cancellationToken)
        {
            //check if port exists
            var oldPort = await _context.Ports.FindAsync(request.Id);
            if (oldPort == null)
            {
                throw new GhionException(CustomResponse.NotFound("Port"));
            }
            // update port
            oldPort.PortNumber = request.PortNumber;
            oldPort.Country = request.Country;
            oldPort.Region = request.Region;
            oldPort.Vollume = request.Vollume;

            _context.Ports.Update(oldPort);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Port Updated Successfully!");
        }
    }
}
