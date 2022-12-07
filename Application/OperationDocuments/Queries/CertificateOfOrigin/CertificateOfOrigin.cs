
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.CertificateOfOrigin;

public record CertificateOfOrigin : IRequest<CertificateDto> {
    public int operationId {get; set;}
}

public class CertificateOfOriginHandler : IRequestHandler<CertificateOfOrigin, CertificateDto> {

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;

    public CertificateOfOriginHandler(IAppDbContext context, OperationEventHandler operationEvent) {
        _context = context;
        _operationEvent = operationEvent;
    }
    
    public async Task<CertificateDto> Handle(CertificateOfOrigin request, CancellationToken cancellationToken) {
        
        var operation = _context.Operations.Where(d => d.Id == request.operationId)
            .Include(o => o.Company)
            .Include(o => o.Company.ContactPerson)
            .Include(o => o.Company.Address)
            .Include(o => o.Goods)
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
                Company = new Company {
                    Name = o.Company.Name,
                    TinNumber = o.Company.TinNumber,
                    CodeNIF = o.Company.CodeNIF,
                    Address = new Address {
                        Phone = o.Company.Address.Phone,
                        City = o.Company.Address.City,
                        Country = o.Company.Address.Country
                    },
                    ContactPerson = new ContactPerson {
                        Name = o.Company.ContactPerson.Name,
                        TinNumber = o.Company.ContactPerson.TinNumber
                    }
                },
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
                    LocationPortId = g.LocationPortId,
                    // Container = new Container {
                    //     ContianerNumber = g.Container.ContianerNumber,
                    //     SealNumber = g.Container.SealNumber,
                    //     Location = g.Container.Location,
                    //     Size = g.Container.Size,
                    //     LocationPortId = g.Container.LocationPortId,
                    //     IsAssigned = g.Container.IsAssigned,
                    //     OperationId = g.Container.OperationId,
                    //     TruckAssignmentId = g.Container.TruckAssignmentId
                    // }
                }).ToList() : null,
            }).FirstOrDefault();

        if(operation == null) {
            throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
        }

        // var goods = await _context.Goods.Where(g => g.OperationId == request.operationId).ToListAsync();

        return new CertificateDto {
            operation = operation,
            goods = operation.Goods
        };

    }
}