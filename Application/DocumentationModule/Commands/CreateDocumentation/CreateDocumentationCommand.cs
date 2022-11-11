

using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.DocumentationModule.Commands.CreateDocumentation
{
    public record CreateDocumentationCommand : IRequest<int>
    {
        
        public int OperationId { get; init; }
        public string Type { get; init; } 
        public DateTime Date { get; init; }
        public string? BankPermit { get; init; }
        public string? InvoiceNumber { get; init; }
        public string? ImporterName { get; init; }
        public string? Phone { get; init; }
        public string? Country { get; init; }
        public string? City { get; init; }
        public string? TinNumber { get; init; }
        public string? TransportationMethod { get; init; }
        public string? Source { get; init; }
        public string? Destination { get; init; }
    }
    public class CreateDocumentationCommandHandler : IRequestHandler<CreateDocumentationCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public CreateDocumentationCommandHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
           _mapper = mapper;
        }
        public async  Task<int> Handle(CreateDocumentationCommand request, CancellationToken cancellationToken)
        {
            Documentation newDocumentation = _mapper.Map<Documentation>(request);
            await _context.Documentations.AddAsync(newDocumentation);
            await _context.SaveChangesAsync(cancellationToken);
            return newDocumentation.Id;

        }
    }
}
