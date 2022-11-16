using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.PaymentModule.Commands.DeletePayment
{
public record DeletePaymentCommand : IRequest<bool>{
    public int Id {get; init;}
}
    public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeletePaymentCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
           var payment = await _context.Payments.FindAsync(request.Id);
           if(payment == null){
            throw new NotFoundException("Payment" , new{Id = request.Id});
           }
           _context.Payments.Remove(payment);
           await _context.SaveChangesAsync(cancellationToken);
           return true;
        }
    }
}