
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.OperationModule.Commands.CreateOperation
{
    public record CreateOperationCommand : IRequest<CustomResponse>
    {
        public string? NameOnPermit { get; set; }
        public string? Consignee { get; set; }
        public string? NotifyParty { get; set; }
        public string BillNumber { get; set; }
        public string? ShippingLine { get; set; }
        public string? GoodsDescription { get; set; }
        public float? Quantity { get; set; }
        public float? GrossWeight { get; set; }
        public string? ATA { get; set; }
        public string? FZIN { get; set; }
        public string? FZOUT { get; set; }
        public string DestinationType { get; set; }
        public byte[]? SourceDocument { get; set; }
        public DateTime? ActualDateOfDeparture { get; set; }
        public DateTime? EstimatedTimeOfArrival { get; set; }
        public string? VoyageNumber { get; set; }
        public string? TypeOfMerchandise { get; set; }
        public DateTime? OpenedDate { get; set; }
        public byte[]? ECDDocument { get; set; }
        public int? ShippingAgentId { get; set; }
        public int? PortOfLoadingId { get; set; }
        public int CompanyId { get; set; }
    }
    public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUploadService;
        private readonly ILogger<CreateOperationCommandHandler> _logger;

        public CreateOperationCommandHandler(IAppDbContext context, IMapper mapper, IFileUploadService fileUploadService, ILogger<CreateOperationCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
            _logger = logger;
        }
        public async Task<CustomResponse> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
        {

            var executionStrategy = _context.database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.database.BeginTransaction())
                {
                    try
                    {
                        //create operation with empty operation number
                        Operation newOperation = _mapper.Map<Operation>(request);
                        newOperation.OperationNumber = "";
                        newOperation.Status = Enum.GetName(typeof(Status) , Status.Opened);
                        newOperation.OpenedDate =  newOperation.OpenedDate != null ? newOperation.OpenedDate: DateTime.Now;
                        await _context.Operations.AddAsync(newOperation);
                        await _context.SaveChangesAsync(cancellationToken);
                        
                        // update operation number to a unique value
                        newOperation.OperationNumber = GenerateOperationNumber(newOperation.Id, request.DestinationType);
                        _context.Operations.Update(newOperation);
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync();

                        return CustomResponse.Succeeded("operation created successfully", 201);
                    }
                    catch (System.Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }




                }
            });

        }
        private string GenerateOperationNumber(int operationId, string destinationType)
        {
            var DestinatinTypes = Enum.GetNames(typeof(DestinationType)).ToList();
            string prefix = "";
            DestinatinTypes.ForEach(dt =>
            {
                if (dt.ToUpper() == destinationType.ToUpper())
                {
                    prefix = dt.Substring(0, 2);
                }
            });
            if (prefix == "")
            {
                throw new GhionException(CustomResponse.BadRequest("invalid destination type"));
            }
            string YY = DateTime.Now.Year.ToString().Substring(2);
            string id = operationId.ToString("D" + 4);
            return prefix + YY + id;
        }
    }
}
