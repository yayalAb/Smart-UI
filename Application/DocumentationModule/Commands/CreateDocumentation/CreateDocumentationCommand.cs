

using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.DocumentationModule.Commands.CreateDocumentation
{
    public record CreateDocumentationCommand : IRequest<CustomResponse>
    {

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
    public class CreateDocumentationCommandHandler : IRequestHandler<CreateDocumentationCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public CreateDocumentationCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CustomResponse> Handle(CreateDocumentationCommand request, CancellationToken cancellationToken)
        {
            Documentation newDocumentation = _mapper.Map<Documentation>(request);
            await _context.Documentations.AddAsync(newDocumentation);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("documentation created successfully");

        }
    }
}
