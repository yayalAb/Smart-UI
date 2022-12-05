
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.PackageList;

public record PackageList : IRequest<PackageListDto> {
    public int operationId {get; init;}
}

public class PackageListHandler : IRequestHandler<PackageList, PackageListDto> {

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;

    public PackageListHandler(IAppDbContext context, OperationEventHandler operationEvent) {
        _context = context;
        _operationEvent = operationEvent;
    }

    public async Task<PackageListDto> Handle(PackageList request, CancellationToken cancellationToken) {

        var operation = await _context.Operations.Where(d => d.Id == request.operationId)
            .Include(o => o.Goods)
            .Include(o => o.Containers)
            .Select(o => new Operation {
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
                Goods = (o.Goods != null) ? o.Goods.Select(g => new Good {
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
                }).ToList() : null,
                Containers = (o.Containers != null) ? o.Containers.Select(c => new Container {
                    ContianerNumber = c.ContianerNumber,
                    SealNumber = c.SealNumber,
                    Location = c.Location,
                    Size = c.Size,
                    LocationPortId = c.LocationPortId,
                    IsAssigned = c.IsAssigned,
                    OperationId = c.OperationId,
                    TruckAssignmentId = c.TruckAssignmentId
                }).ToList() : null
            }).FirstAsync();

        if(operation == null){
            throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
        }

        var doc = _context.Documentations.Where(d => d.OperationId == request.operationId && d.Type == Enum.GetName(typeof(Documents), Documents.PackageList)).Select(d => new Documentation {
            OperationId = d.OperationId,
            Type = d.Type,
            Date = d.Date,
            BankPermit = d.BankPermit,
            InvoiceNumber = d.InvoiceNumber,
            // ImporterName = d.ImporterName,
            // Phone = d.Phone,
            // Country = d.Country,
            // City = d.City,
            // TinNumber = d.TinNumber,

            Source = d.Source,
            Destination = d.Destination
        }).FirstOrDefault();
        
        if(doc == null){
            throw new GhionException(CustomResponse.Failed("Documentaion Not found!", 450));
        }

        return new PackageListDto {
            documentation = doc,
            operation = operation,
            goods = operation.Goods,
            containers = operation.Containers
        };

    }
}