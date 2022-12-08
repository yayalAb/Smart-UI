
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

namespace Application.OperationModule.Commands.UpdateOperation

{
    public record UpdateOperationCommand : IRequest<CustomResponse>
    {
        public int Id {get; set;}
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
        public string? ConnaissementNumber { get; set; }
        public string? CountryOfOrigin { get; set; }
        public float? REGTax { get; set; }
        public string? BillOfLoadingNumber { get; set; }
        public string? FinalDestination { get; set; }
        public string? Localization { get; set; }
        public string? PINumber { get; set; } = null;
        public DateTime? PIDate { get; set; } = null;

        //--------------------------------------//
    }
    public class UpdateOperationCommandHandler : IRequestHandler<UpdateOperationCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUploadService;

        public UpdateOperationCommandHandler(IAppDbContext context, IMapper mapper, IFileUploadService fileUploadService)
        {
            _context = context;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
        }
        public async Task<CustomResponse> Handle(UpdateOperationCommand request, CancellationToken cancellationToken) {

            var found_operation = await _context.Operations.FindAsync(request.Id);

            if (found_operation == null) {
                throw new GhionException(CustomResponse.NotFound($"operation with Id = {request.Id} is not found"));
            }

            var operation = new Operation {
                NameOnPermit = found_operation.NameOnPermit,
                Consignee = request.Consignee,
                NotifyParty = request.NotifyParty,
                BillNumber = request.BillNumber,
                ShippingLine = request.ShippingLine,
                GoodsDescription = request.GoodsDescription,
                Quantity = request.Quantity,
                GrossWeight = request.GrossWeight,
                ATA = request.ATA,
                FZIN = request.FZIN,
                FZOUT = request.FZOUT,
                DestinationType = request.DestinationType,
                SourceDocument =  request.SourceDocument,
                ActualDateOfDeparture = request.ActualDateOfDeparture,
                EstimatedTimeOfArrival = request.EstimatedTimeOfArrival,
                VoyageNumber = request.VoyageNumber,
                TypeOfMerchandise = request.TypeOfMerchandise,
                OperationNumber = found_operation.OperationNumber,
                OpenedDate = request.OpenedDate,
                Status = found_operation.Status,
                ECDDocument = request.ECDDocument,
                ShippingAgentId = request.ShippingAgentId,
                PortOfLoadingId = request.PortOfLoadingId,
                CompanyId = request.CompanyId,
                /////------------Additionals------
                SNumber = request.SNumber,
                SDate = request.SDate,
                RecepientName = request.RecepientName,
                VesselName = request.VesselName,
                ArrivalDate = request.ArrivalDate,
                ConnaissementNumber = request.ConnaissementNumber,
                CountryOfOrigin = request.CountryOfOrigin,
                REGTax = request.REGTax,
                BillOfLoadingNumber = request.BillOfLoadingNumber,
                PINumber = found_operation.PINumber,
                PIDate = found_operation.PIDate,
                FinalDestination = request.FinalDestination,
                Localization = request.Localization,
            };

            // Operation updatedOperation = _mapper.Map<Operation>(request);
            _context.Operations.Update(operation);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("operation updated successfully!");

        }
    }
}
