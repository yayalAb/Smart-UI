
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PaymentModule.Commands.UpdatePayment

{
    public record UpdatePymentCommand : IRequest<CustomResponse>
    {
        public int Id { get; set; } 
        public string Type { get; init; }
        public string Name { get; set; }    
        public DateTime PaymentDate { get; init; }
        public string PaymentMethod { get; init; }
        public string? BankCode { get; init; }
        public float Amount { get; init; }
        public string Currency { get; init; }
        public string? Description { get; init; }
        public int OperationId { get; init; }
        public int ShippingAgentId { get; init; }
        public string? DONumber { get; set; }
    }
    public class UpdatePymentCommandHandler : IRequestHandler<UpdatePymentCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public UpdatePymentCommandHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CustomResponse> Handle(UpdatePymentCommand request, CancellationToken cancellationToken)
        {
            var oldPayment = await _context.Payments.AsNoTracking().FirstOrDefaultAsync(s => s.Id == request.Id);
            
            if (oldPayment == null) {
                throw new GhionException(CustomResponse.NotFound("payment not found"));
            }

            oldPayment = _mapper.Map<Payment>(request);
             _context.Payments.Update(oldPayment);
            await _context.SaveChangesAsync(cancellationToken);

            return CustomResponse.Succeeded("Payment Updated");

        }
    }
}
