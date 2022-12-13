
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;

namespace Application.OperationModule.Commands.UpdateOperation

{
    public record UpdateOperationCommand : IRequest<CustomResponse>
    {
        public int Id {get; set;}
        public int ContactPersonId { get; set; }
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
        public string? SourceDocument { get; set; }
        public DateTime? ActualDateOfDeparture { get; set; }
        public DateTime? EstimatedTimeOfArrival { get; set; }
        public string? VoyageNumber { get; set; }
        public string? TypeOfMerchandise { get; set; }
        public DateTime OpenedDate { get; set; }
        public string? ECDDocument { get; set; }
        public int? ShippingAgentId { get; set; }
        public int? PortOfLoadingId { get; set; }
        public int CompanyId { get; set; }
        /////------------Additionals------
        public string? SNumber { get; set; }
        public DateTime? SDate { get; set; }
        public string? RecepientName { get; set; }
        public string? VesselName { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string? CountryOfOrigin { get; set; }
        public float? REGTax { get; set; }
        public string? BillOfLoadingNumber { get; set; }
        public string? FinalDestination { get; set; }
        public string? Localization { get; set; }
        public string? Shipper { get; set; }
        public string? PINumber { get; set; } = null;
        public DateTime? PIDate { get; set; } = null;

        //--------------------------------------//
    }
    public class UpdateOperationCommandHandler : IRequestHandler<UpdateOperationCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUploadService;

        public UpdateOperationCommandHandler(IAppDbContext context, IMapper mapper, IFileUploadService fileUploadService) {
            _context = context;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
        }
        public async Task<CustomResponse> Handle(UpdateOperationCommand request, CancellationToken cancellationToken) {

            var found_operation = await _context.Operations.FindAsync(request.Id);

            if (found_operation == null) {
                throw new GhionException(CustomResponse.NotFound($"operation with Id = {request.Id} is not found"));
            }

            // found_operation = _mapper.Map<Operation>(request);
            found_operation.ContactPersonId = request.ContactPersonId;
            found_operation.Consignee = request.Consignee;
            found_operation.NotifyParty = request.NotifyParty;
            found_operation.BillNumber = request.BillNumber;
            found_operation.ShippingLine = request.ShippingLine;
            found_operation.GoodsDescription = request.GoodsDescription;
            found_operation.Quantity = request.Quantity;
            found_operation.GrossWeight = request.GrossWeight;
            found_operation.ATA = request.ATA;
            found_operation.FZIN = request.FZIN;
            found_operation.FZOUT = request.FZOUT;
            found_operation.DestinationType = request.DestinationType;
            found_operation.SourceDocument =  request.SourceDocument;
            found_operation.ActualDateOfDeparture = request.ActualDateOfDeparture;
            found_operation.EstimatedTimeOfArrival = request.EstimatedTimeOfArrival;
            found_operation.VoyageNumber = request.VoyageNumber;
            found_operation.TypeOfMerchandise = request.TypeOfMerchandise;
            found_operation.OpenedDate = request.OpenedDate;
            found_operation.ECDDocument = request.ECDDocument;
            found_operation.ShippingAgentId = request.ShippingAgentId;
            found_operation.PortOfLoadingId = request.PortOfLoadingId;
            found_operation.CompanyId = request.CompanyId;
            /////------------Additionals------
            found_operation.SNumber = request.SNumber;
            found_operation.SDate = request.SDate;
            found_operation.RecepientName = request.RecepientName;
            found_operation.VesselName = request.VesselName;
            found_operation.ArrivalDate = request.ArrivalDate;
            found_operation.CountryOfOrigin = request.CountryOfOrigin;
            found_operation.REGTax = request.REGTax;
            found_operation.BillOfLoadingNumber = request.BillOfLoadingNumber;
            found_operation.FinalDestination = request.FinalDestination;
            found_operation.Localization = request.Localization;

            // Operation updatedOperation = _mapper.Map<Operation>(request);
            // _context.Operations.Update(found_operation);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("operation updated successfully!");

        }
    }
}
