

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.ShippingAgentModule.Commands.DeleteShippingAgent
{
    public record DeleteShippingAgentCommand : IRequest<CustomResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteShippingAgentCommandHndler : IRequestHandler<DeleteShippingAgentCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public DeleteShippingAgentCommandHndler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(DeleteShippingAgentCommand request, CancellationToken cancellationToken)
        {
            var found_ShippingAgent = await _context.ShippingAgents.FindAsync(request.Id);
            if (found_ShippingAgent == null)
            {
                throw new GhionException(CustomResponse.NotFound($"ShippingAgent with id = {request.Id} is not found"));
            }
            _context.ShippingAgents.Remove(found_ShippingAgent);
            await _context.SaveChangesAsync(cancellationToken);

            return CustomResponse.Succeeded("ShippingAgent deleted successfully!");
        }
    }

}
