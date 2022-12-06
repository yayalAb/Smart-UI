
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Application.OperationFollowupModule;
using Domain.Entities;
using Domain.Enums;

namespace Application.OperationDocuments.Queries.TruckWayBill;

public record TruckWayBill : IRequest<TruckWayBillDto>
{
    public int operationId { get; init; }
    public int TruckAssignmentId { get; init; }
    public bool Type {get; init;} = false!;
}

public class TruckWayBillHandler : IRequestHandler<TruckWayBill, TruckWayBillDto>
{

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;

    public TruckWayBillHandler(IAppDbContext context, OperationEventHandler operationEvent)
    {
        _context = context;
        _operationEvent = operationEvent;
    }

    public async Task<TruckWayBillDto> Handle(TruckWayBill request, CancellationToken cancellationToken)
    {

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
        }else if(request.Type && operation.Status != Enum.GetName(typeof(Status), Status.ECDDispatched)){
            throw new GhionException(CustomResponse.NotFound("ECD Document should be dispached!"));
        }

        Documentation doc = null;
        if(!request.Type){
            
            doc = _context.Documentations.Where(d => d.OperationId == request.operationId).Select(d => new Documentation {
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

            if (doc == null) {
                throw new GhionException(CustomResponse.Failed("Documentaion Not found!", 450));
            }

        }

        var assignment = await _context.TruckAssignments
                                        .Include(t => t.Driver)
                                        .Include(t => t.Truck)
                                        .Include(t => t.SourcePort)
                                        .Include(t => t.DestinationPort)
                                        .Include(t => t.Containers)
                                        .Include(t => t.Goods)
                                        .Where(g => g.Id == request.TruckAssignmentId)
                                        .Select(t => new TruckAssignment {
                                            DriverId = t.DriverId,
                                            TruckId = t.TruckId,
                                            OperationId = t.OperationId,
                                            SourceLocation = t.SourceLocation,
                                            DestinationLocation = t.DestinationLocation,
                                            SourcePortId = t.SourcePortId,
                                            DestinationPortId = t.DestinationPortId,
                                            Truck =  new Truck {
                                                TruckNumber = t.Truck.TruckNumber,
                                                Type = t.Truck.Type,
                                                PlateNumber  = t.Truck.PlateNumber,
                                                Capacity = t.Truck.Capacity,
                                                Image = t.Truck.Image,
                                                IsAssigned = t.Truck.IsAssigned
                                            },
                                            Containers = (t.Containers != null) ? t.Containers.Select(c => new Container() {
                                                ContianerNumber = c.ContianerNumber,
                                                SealNumber = c.SealNumber,
                                                Location = c.Location,
                                                Size = c.Size,
                                                LocationPortId = c.LocationPortId,
                                                IsAssigned = c.IsAssigned,
                                                OperationId = c.OperationId,
                                                TruckAssignmentId = c.TruckAssignmentId
                                            }).ToList() : null,
                                            Goods = (t.Goods != null) ? t.Goods.Select(g => new Good {
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
                                            }).ToList() : null
                                        }).FirstAsync();

        var containers = await _context.Containers.Where(c => c.OperationId == request.operationId).ToListAsync();

        if (assignment == null) {
            throw new GhionException(CustomResponse.NotFound("Assignment not Found!"));
        }

        TruckWayBillDto bill = new TruckWayBillDto {
            operation = operation
        };

        if (assignment.Containers != null) {
            bill.containers = assignment.Containers;
        }

        if (doc != null) {
            bill.documentation = doc;
        }

        if (assignment.Goods != null) {
            bill.goods = assignment.Goods;
        }
        
        if(request.Type){
            await _operationEvent.DocumentGenerationEventAsync(cancellationToken, new OperationStatus {
                GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.Waybill),
                GeneratedDate = DateTime.Now,
                IsApproved = true,
                OperationId = doc.OperationId
            }, Enum.GetName(typeof(Status), Status.Closed));
        }

        return bill;

    }
}