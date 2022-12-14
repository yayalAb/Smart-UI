
using System.Reflection.Metadata;
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
    public record CreateOperationCommand : IRequest<object>
    {
        public int ContactPersonId { get; set; }
        public string? Consignee { get; set; }
        public string? NotifyParty { get; set; }
        public string BillNumber { get; set; }
        public string? ShippingLine { get; set; }
        public string? GoodsDescription { get; set; }
        public float Quantity { get; set; } = 0;
        public float GrossWeight { get; set; } = 0;
        public string? ATA { get; set; }
        public string? FZIN { get; set; }
        public string? FZOUT { get; set; }
        public string DestinationType { get; set; }
        public string? SourceDocument { get; set; }
        public DateTime? ActualDateOfDeparture { get; set; }
        public DateTime? EstimatedTimeOfArrival { get; set; }
        public string? VoyageNumber { get; set; }
        //--********************----------------////
        public string OperationNumber { get; set; }
        public DateTime OpenedDate { get;set ; }
        public string Status { get; set; }
        //--********************--------------------///
        public string? ECDDocument { get; set; }
        public int? ShippingAgentId { get; set; }
        public int PortOfLoadingId { get; set; }
        public int CompanyId { get; set; }
        /////------------Additionals------
        public string? SNumber { get; set; }
        public DateTime? SDate { get; set; }
        public string? RecepientName { get; set; }
        public string? VesselName { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string? CountryOfOrigin { get; set; }
        public float REGTax { get; set; } = 0;
        public string? BillOfLoadingNumber { get; set; }
        public string? FinalDestination { get; set; }
        public string? Localization { get; set; }
        public string? Shipper { get; set; }
        public string? PINumber { get; set; } = null;
        public DateTime? PIDate { get; set; } = null;
    }
    public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, object>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        private readonly IFileUploadService _fileUploadService;
        private readonly OperationService _operationService;
        private readonly ILogger<CreateOperationCommandHandler> _logger;

        public CreateOperationCommandHandler(IAppDbContext context, IMapper mapper, IFileUploadService fileUploadService,OperationService operationService, ILogger<CreateOperationCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
            _operationService = operationService;
            _logger = logger;
        }
        public async Task<object> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
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
                        newOperation.Status = Enum.GetName(typeof(Status), Status.Opened)!;
                        newOperation.OpenedDate = DateTime.Now;
                        await _context.Operations.AddAsync(newOperation);
                        await _context.SaveChangesAsync(cancellationToken);

                        // update operation number to a unique value
                        newOperation.OperationNumber = _operationService.GenerateOperationNumber(newOperation.Id, request.DestinationType);
                        _context.Operations.Update(newOperation);
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync();

                        return new {Id = newOperation.Id , operationNumber = newOperation.OperationNumber};
                    }
                    catch (System.Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }

                }
            });

        }
      
}
}
