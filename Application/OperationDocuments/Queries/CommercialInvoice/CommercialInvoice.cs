
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.CommercialInvoice;

public record CommercialInvoice : IRequest<CommercialInvoiceDto> {
    public int operationId {get; init;}
    //if type is false it means Commercia invoice if it is true it means Proforma invoice
    public bool Type {get; init;} = false;
}

public class CommercialInvoiceHandler : IRequestHandler<CommercialInvoice, CommercialInvoiceDto> {

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;

    public CommercialInvoiceHandler(IAppDbContext context, OperationEventHandler operationEvent) {
        _context = context;
        _operationEvent = operationEvent;
    }

    public async Task<CommercialInvoiceDto> Handle(CommercialInvoice request, CancellationToken cancellationToken) {
        
        var operation = _context.Operations.Where(d => d.Id == request.operationId).Select(o => new Operation {
            Id = o.Id,
            NameOnPermit = o.NameOnPermit,
            Consignee = o.Consignee,
            NotifyParty = o.NotifyParty,
            BillNumber = o.BillNumber,
            ShippingLine = o.ShippingLine,
            GoodsDescription = o.GoodsDescription,
            Quantity = o.Quantity,
            GrossWeight = o.GrossWeight,
            ATA = o.ATA,
            FZIN = o.FZIN,
            FZOUT = o.FZOUT,
            DestinationType = o.DestinationType,
            SourceDocument = o.SourceDocument,
            ActualDateOfDeparture = o.ActualDateOfDeparture,
            EstimatedTimeOfArrival = o.EstimatedTimeOfArrival,
            VoyageNumber = o.VoyageNumber,
            TypeOfMerchandise = o.TypeOfMerchandise,
            OperationNumber = o.OperationNumber,
            OpenedDate = o.OpenedDate,
            Status = o.Status,
            ECDDocument = o.ECDDocument,
            ShippingAgentId = o.ShippingAgentId,
            PortOfLoadingId = o.PortOfLoadingId,
            CompanyId = o.CompanyId,
            /////------------Additionals------
            SNumber = o.SNumber, // operation
            SDate = o.SDate, //operation
            RecepientName = o.RecepientName,
            VesselName = o.VesselName, // operation
            ArrivalDate = o.ArrivalDate, // operation
            ConnaissementNumber = o.ConnaissementNumber, // operation
            CountryOfOrigin = o.CountryOfOrigin, // operation
            REGTax = o.REGTax,//
            BillOfLoadingNumber = o.BillOfLoadingNumber,
            Company = o.Company
        }).FirstOrDefault();

        if(operation == null){
            throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
        }

        var doc = _context.Documentations.Where(d => d.OperationId == request.operationId && d.Type == Enum.GetName(typeof(Documents), !request.Type ? Documents.CommercialInvoice : Documents.ProformaInvoice)).Select(d => new Documentation {
            OperationId = d.OperationId,
            Type = d.Type,
            Date = d.Date,
            BankPermit = d.BankPermit,
            InvoiceNumber = d.InvoiceNumber,
            ImporterName = d.ImporterName,
            Phone = d.Phone,
            Country = d.Country,
            City = d.City,
            TinNumber = d.TinNumber,
            TransportationMethod = d.TransportationMethod,
            Source = d.Source,
            Destination = d.Destination,
        }).FirstOrDefault();
        
        if(doc == null){
            throw new GhionException(CustomResponse.Failed("Documentaion Not found!", 450));
        }

        var goods = await _context.Goods.Where(g => g.OperationId == request.operationId).Include(g => g.Container).Select(g => new Good {
            Description = g.Description,
            HSCode = g.HSCode,
            Manufacturer = g.Manufacturer,
            Weight = g.Weight,
            Quantity = g.Quantity,
            NumberOfPackages = g.NumberOfPackages,
            Type = g.Type,
            Location = g.Location,
            ChasisNumber = g.ChasisNumber,
            EngineNumber = g.EngineNumber,
            ModelCode = g.ModelCode,
            IsAssigned = g.IsAssigned,
            ContainerId = g.ContainerId,
            OperationId = g.OperationId,
            TruckAssignmentId = g.TruckAssignmentId,
            LocationPortId = g.LocationPortId
        }).ToListAsync();

        var containers = await _context.Containers.Where(c => c.OperationId == request.operationId).Select(c => new Container {
                ContianerNumber = c.ContianerNumber,
                SealNumber = c.SealNumber,
                Location = c.Location,
                Size = c.Size,
                LocationPortId = c.LocationPortId,
                IsAssigned = c.IsAssigned,
                OperationId = c.OperationId,
                TruckAssignmentId = c.TruckAssignmentId
            }).ToListAsync();

        return new CommercialInvoiceDto {
            Document = doc,
            Operation = operation,
            Goods = goods,
            Containers = containers
        };

    }
}