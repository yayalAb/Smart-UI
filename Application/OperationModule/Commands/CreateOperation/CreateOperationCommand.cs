
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.OperationModule.Commands.CreateOperation
{
    public record CreateOperationCommand : IRequest<int>
    {
        public string CustomerName { get; init; }
        public string? NameOnPermit { get; init; }
        public string? Consignee { get; init; }
        public string NotifyParty { get; init; }
        public string BillNumber { get; init; }
        public string ShippingLine { get; init; }
        public string GoodsDescription { get; init; }
        public float Quantity { get; init; }
        public float? GrossWeight { get; init; }
        public string? ATA { get; init; }
        public string? FZIN { get; init; }
        public string? FZOUT { get; init; }
        public string DestinationType { get; init; }
        public byte[]? SourceDocument { get; init; }
        public string SourceDocumentType { get; init; }
        public DateTime? ActualDateOfDeparture { get; init; }
        public DateTime? EstimatedTimeOfArrival { get; init; }
        public string VoyageNumber { get; init; }
        public string TypeOfMerchandise { get; init; }
        public string OperationNumber { get; init; } = null!;
        public DateTime OpenedDate { get; init; }
        public string Status { get; init; } = null!;
        public byte[]? ECDDocument { get; init; }
        public int ShippingAgentId { get; init; }
        public int PortOfLoadingId { get; init; }
        public int CompanyId { get; init; }
    }
    public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public CreateOperationCommandHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
        {
            Operation newOperation = _mapper.Map<Operation>(request);
            await _context.Operations.AddAsync(newOperation);
            await _context.SaveChangesAsync(cancellationToken);
            return newOperation.Id;
        }
    }
}
