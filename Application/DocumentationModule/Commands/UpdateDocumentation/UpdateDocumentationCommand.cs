
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.DocumentationModule.Commands.UpdateDocumentation
{
    public record UpdateDocumentationCommand : IRequest<CustomResponse>
    {
        public int Id { get; init; }
        public int OperationId { get; init; }
        public string Type { get; init; }
        public DateTime Date { get; init; }
        public string? BankPermit { get; init; }
        public string? InvoiceNumber { get; init; }
        public string? Source { get; init; }
        public string? Destination { get; init; }

        public string? PurchaseOrderNumber { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public string? PaymentTerm { get; set; }
        public bool? IsPartialShipmentAllowed { get; set; }
        public string? Fright { get; set; }
    }
    public class UpdateDocumentationCommandHandler : IRequestHandler<UpdateDocumentationCommand, CustomResponse>
    {

        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public UpdateDocumentationCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CustomResponse> Handle(UpdateDocumentationCommand request, CancellationToken cancellationToken)
        {
            //check if Documentation exists
            if (!_context.Documentations.Where(d => d.Id == request.Id).Any())
            {
                throw new GhionException(CustomResponse.NotFound("Documentation not found!"));
            }
            _context.Documentations.Update(_mapper.Map<Documentation>(request));
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("documentation updated");
        }
    }

}
