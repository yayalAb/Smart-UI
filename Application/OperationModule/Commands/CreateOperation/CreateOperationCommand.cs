
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.OperationModule.Commands.CreateOperation
{
    public record CreateOperationCommand : IRequest<int>
    {
        public string OperationNumber { get; set; } = null!;
        public DateTime OpenedDate { get; set; }
        public int BillOfLoadingId { get; set; }
    }
    public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateOperationCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
        {
            Operation newOperation = new Operation
            {
                OperationNumber = request.OperationNumber,
                OpenedDate = request.OpenedDate,
                BillOfLoadingId = request.BillOfLoadingId,
                Status = OperationStatus.Opened.ToString()
            };
            await _context.Operations.AddAsync(newOperation);
            await _context.SaveChangesAsync(cancellationToken);
            return newOperation.Id;
        }
    }
}
