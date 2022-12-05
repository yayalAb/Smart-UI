
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationFollowupModule;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationDocuments.Queries.CommercialInvoice;

public record CommercialInvoice : IRequest<CommercialInvoiceDto2>
{
    public int operationId { get; init; }
    //if type is false it means Commercia invoice if it is true it means Proforma invoice
    public string? PINumber { get; init; }
    public DateTime? PIDate { get; init; }
    public int TruckAssignmentId { get; init; }
    public bool Type { get; init; } = false;
}

public class CommercialInvoiceHandler : IRequestHandler<CommercialInvoice, CommercialInvoiceDto2>
{

    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEvent;

    public CommercialInvoiceHandler(IAppDbContext context, OperationEventHandler operationEvent)
    {
        _context = context;
        _operationEvent = operationEvent;
    }

    public async Task<CommercialInvoiceDto2> Handle(CommercialInvoice request, CancellationToken cancellationToken)
    {
        if(!await _context.TruckAssignments.Where(ta => ta.Id == request.TruckAssignmentId).AnyAsync())
        {
            throw new GhionException( CustomResponse.NotFound($"truck assignment with id = {request.TruckAssignmentId} is not found "));
        }
        var op = _context.Operations
            .Where(d => d.Id == request.operationId).FirstOrDefault();
        if (op == null)
        {
            throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
        }
        if (request.PINumber != null)
        {
            op.PINumber = request.PINumber;
            op.PIDate = request.PIDate;
            _context.Operations.Update(op);
            await _context.SaveChangesAsync(cancellationToken);
        }
        if (op.PINumber == null)
        {
            throw new GhionException(CustomResponse.BadRequest("pi number cannot be null"));

        }
        var commercialInvoiceData = await _context.Operations       
                        .Include(o => o.Company)
                            .ThenInclude(c => c.ContactPerson)
                        .Include(o => o.TruckAssignments!.Where(ta => ta.Id == request.TruckAssignmentId))
                            .ThenInclude(ta => ta.Goods)!
                                .ThenInclude(g => g.Container)
                        .Include(o => o.PortOfLoading)
                        .Where(d => d.Id == request.operationId)
                        .Select(o => new CommercialInvoiceDto2{
                            OperationNumber = o.OperationNumber,
                            PINumber = o.PINumber,
                            PIDate = o.PIDate ,
                            CustomerName = o.Company.ContactPerson.Name,
                            CustomerAddress = string.Concat(o.Company.ContactPerson.City ," , ", o.Company.ContactPerson.Country) ,
                            CustomerPhone = o.Company.ContactPerson.Phone,
                            CustomerTinNumber = o.Company.ContactPerson.TinNumber,
                            PortOfLoading = o.PortOfLoading.Country,
                            FinalDestination = o.FinalDestination,
                            TransportationMethod =o.TruckAssignments == null
                                                ?null
                                                : o.TruckAssignments.FirstOrDefault()!.TransportationMethod,
                            Goods = o.Goods.Select(g => new CIGoodsDto{
                                Description = g.Description,
                                HSCode = g.HSCode,
                                Quantity = g.NumberOfPackages ,
                                Unit = g.Unit ,
                                UnitPrice = g.UnitPrice,
                                ContainerNumber = g.Container == null
                                            ?null
                                            :g.Container.ContianerNumber
                            })
                            
                        })
                        .FirstOrDefaultAsync();

        var doc = _context.Documentations.Where(d => d.OperationId == request.operationId && d.Type == Enum.GetName(typeof(Documents), !request.Type ? Documents.CommercialInvoice : Documents.ProformaInvoice)).Select(d => new Documentation
        {
            Date = d.Date,
            PurchaseOrderDate = d.PurchaseOrderDate,
            PurchaseOrderNumber = d.PurchaseOrderNumber,
            PaymentTerm = d.PaymentTerm ,
            IsPartialShipmentAllowed = d.IsPartialShipmentAllowed 
        }).FirstOrDefault();
      
        if (doc == null)
        {
            throw new GhionException(CustomResponse.Failed("Documentaion Not found!", 450));
        }
        commercialInvoiceData!.Date = doc.Date;
        commercialInvoiceData.PurchaseOrderDate = doc.PurchaseOrderDate;
        commercialInvoiceData.PurchaseOrderNumber = doc.PurchaseOrderNumber;
        commercialInvoiceData.PaymentTerm = doc.PaymentTerm ;
        commercialInvoiceData.PartialShipment = (bool)doc.IsPartialShipmentAllowed!? "TO BE ALLOWED": "NOT ALLOWED";
        return commercialInvoiceData;

    }
}