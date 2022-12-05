
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Common.PaymentTypes;
using Domain.Entities;
using Domain.Enums;

namespace Application.OperationDocuments.Queries.Number9;

public record Number9 : IRequest<Number9Dto> {
    public int OperationId {get; init;}
    public string Type {get; init;}
}

public class Number9Handler : IRequestHandler<Number9, Number9Dto> {
    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;

    public Number9Handler(IAppDbContext context, OperationEventHandler operationEvent) {
        _context = context;
        _operationEvent = operationEvent;
    }
    
    public async Task<Number9Dto> Handle(Number9 request, CancellationToken cancellationToken) {
        
        var operation = _context.Operations.Where(d => d.Id == request.OperationId)
            .Include(o => o.Company)
            .Include(o => o.Goods)
            .Include(o => o.Containers)
            .Include(o => o.Company.Address)
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
                    ContactPersonId = o.Company.ContactPersonId,
                    AddressId = o.Company.AddressId,
                    Address = new Address{
                        Email = o.Company.Address.Email,
                        Phone = o.Company.Address.Phone,
                        Region = o.Company.Address.Region,
                        City = o.Company.Address.City,
                        Subcity = o.Company.Address.Subcity,
                        Country = o.Company.Address.Country,
                        POBOX = o.Company.Address.POBOX,
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
                    TruckAssignmentId = g.TruckAssignmentId,
                    LocationPortId = g.LocationPortId,
                    Container = g.Container == null 
                                    ? null 
                                    : new Container {
                                        ContianerNumber = g.Container.ContianerNumber,
                                        SealNumber = g.Container.SealNumber,
                                        Location = g.Container.Location,
                                        Size = g.Container.Size,
                                        LocationPortId = g.Container.LocationPortId,
                                        IsAssigned = g.Container.IsAssigned,
                                        OperationId = g.Container.OperationId,
                                        TruckAssignmentId = g.Container.TruckAssignmentId
                                    }
                }).ToList() : null,
            }).First();
        
        if(operation == null){
            throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
        }

        var goods = operation.Goods;
        operation.Goods = null;

        var payment = _context.Payments.Where(c => c.OperationId == request.OperationId && c.Name == ShippingAgentPaymentType.DeliveryOrder ).Select(p => new Payment {
            Name = p.Name,
            Type = p.Type,
            PaymentDate = p.PaymentDate,
            PaymentMethod = p.PaymentMethod,
            BankCode = p.BankCode,
            Amount = p.Amount,
            Currency = p.Currency,
            Description = p.Description,
            OperationId = p.OperationId,
            ShippingAgentId = p.ShippingAgentId,
            DONumber = p.DONumber,
        }).FirstOrDefault();

        if(payment == null){
            throw new GhionException(CustomResponse.NotFound(" Delivery Order Payment for the operation not found!"));
        }

        await _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus {
            GeneratedDocumentName = ((request.Type.ToLower() == "import") ? Enum.GetName(typeof(Documents), Documents.ImportNumber9) : Enum.GetName(typeof(Documents), Documents.TransferNumber9)),
            GeneratedDate = DateTime.Now,
            IsApproved = false,
            OperationId = request.OperationId
        }, ((request.Type.ToLower() == "import") ? Enum.GetName(typeof(Status), Status.ImportNumber9Generated) : Enum.GetName(typeof(Status), Status.Closed)));

        return new Number9Dto {
            company = operation.Company,
            operation = operation,
            goods = goods,
            doPayment = payment
        };

    }
}