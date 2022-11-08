

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeleteAddressCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var existingAddress = await _context.Addresses.FindAsync(request.Id);
            if (existingAddress == null)
            {
                throw new NotFoundException("Address", new { Id = request.Id });

            };

            _context.Addresses.Remove(existingAddress);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}