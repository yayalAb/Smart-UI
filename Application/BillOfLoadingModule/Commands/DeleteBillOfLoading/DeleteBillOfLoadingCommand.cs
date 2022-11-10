
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.BillOfLoadingModule.Commands.DeleteBillOfLoading
{
    public record DeleteBillOfLoadingCommand : IRequest<bool>
    {
        public int Id { get; init; }
    }
    public class DeleteBillOfLoadingCommandHandler : IRequestHandler<DeleteBillOfLoadingCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeleteBillOfLoadingCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteBillOfLoadingCommand request, CancellationToken cancellationToken)
        {
            var existingBillOfLoading = await _context.BillOfLoadings.FindAsync(request.Id);
            if (existingBillOfLoading == null)
            {
                throw new NotFoundException("Bill of loading", new { Id = request.Id });

            };

            _context.BillOfLoadings.Remove(existingBillOfLoading);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
