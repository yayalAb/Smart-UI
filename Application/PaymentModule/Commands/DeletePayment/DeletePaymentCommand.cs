using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.PaymentModule.Commands.DeletePayment
{
public record DeletePaymentCommand : IRequest<CustomResponse>{
    public int Id {get; init;}
}
     public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public DeletePaymentCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
           var found_Payment = await _context.Payments.FindAsync(request.Id);
        if(found_Payment == null){
            throw new GhionException(CustomResponse.NotFound($"Payment with id = {request.Id} is not found"));
        }
        _context.Payments.Remove(found_Payment);
            await _context.SaveChangesAsync(cancellationToken);

         return CustomResponse.Succeeded("Payment deleted successfully!");
        }
    }
}