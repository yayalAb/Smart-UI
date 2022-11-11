
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.DocumentationModule.Commands.UpdateDocumentation
{
    public record UpdateDocumentationCommand : IRequest<int>
    {
         public int Id { get; init; }
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
    public class UpdateDocumentationCommandHandler : IRequestHandler<UpdateDocumentationCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public UpdateDocumentationCommandHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateDocumentationCommand request, CancellationToken cancellationToken)
        {
            //check if Documentation exists
            var oldDoc = await _context.Documentations.FindAsync(request.Id);
            if (oldDoc == null)
            {
                throw new NotFoundException("Documentation", new { id = request.Id });
            }
            // update documentation
            oldDoc.OperationId = request.OperationId;
            oldDoc.InvoiceNumber = request.InvoiceNumber;
            oldDoc.Type = request.Type;
            oldDoc.Date = request.Date;
            oldDoc.BankPermit = request.BankPermit;
            oldDoc.InvoiceNumber = request.InvoiceNumber;
            oldDoc.ImporterName = request.ImporterName;
            oldDoc.Phone = request.Phone;
            oldDoc.Country =request.Country;
            oldDoc.City = request.City;
            oldDoc.TinNumber = request.TinNumber;
            oldDoc.TransportationMethod = request.TransportationMethod;
            oldDoc.Source = request.Source;
            oldDoc.Destination = request.Destination;

            _context.Documentations.Update(oldDoc);
            await _context.SaveChangesAsync(cancellationToken);
            return oldDoc.Id;
        }
    }

}
