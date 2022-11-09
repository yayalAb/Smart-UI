

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.ShippingAgentModule.Commands.DeleteShippingAgent
{
    public record DeleteShippingAgentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteShippingAgentCommandHndler : IRequestHandler<DeleteShippingAgentCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeleteShippingAgentCommandHndler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteShippingAgentCommand request, CancellationToken cancellationToken)
        {
            var existingShippingAgent = await _context.ShippingAgents.FindAsync(request.Id);
            if (existingShippingAgent == null)
            {
                throw new NotFoundException("Shipping Agent", new { Id = request.Id });

            };

            _context.ShippingAgents.Remove(existingShippingAgent);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
