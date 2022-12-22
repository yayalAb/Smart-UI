

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var found_ShippingAgent = await _context.ShippingAgents
                            .Include(sa => sa.Address)
                            .Where(sa => sa.Id == request.Id).FirstOrDefaultAsync();
            if (found_ShippingAgent == null)
            {
                throw new GhionException(CustomResponse.NotFound($"ShippingAgent with id = {request.Id} is not found"));
            }
            var shippingAgentAddress = found_ShippingAgent.Address;
            _context.ShippingAgents.Remove(found_ShippingAgent);
            _context.Addresses.Remove(shippingAgentAddress);
            await _context.SaveChangesAsync(cancellationToken);

            return CustomResponse.Succeeded("ShippingAgent deleted successfully!");
        }
    }

}
