using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationDocuments.Queries.CommercialInvoice;
using Application.OperationDocuments.Queries.PackageList;
using Application.OperationDocuments.Queries.TruckWayBill;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.OperationDocuments.Queries.Common;
public class DocumentationService
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<DocumentationService> _logger;

    public DocumentationService(IAppDbContext context, IMapper mapper, ILogger<DocumentationService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<DocsDto> GetDocumentation(Documents docType, int operationId, int truckAssignmentId, int contactPersonId, CancellationToken cancellationToken)
    {
        if (!await _context.TruckAssignments.Where(ta => ta.Id == truckAssignmentId).AnyAsync())
        {
            throw new GhionException(CustomResponse.NotFound($"truck assignment with id = {truckAssignmentId} is not found "));
        }

        var data = await _context.Operations
                        .Include(o => o.Company)
                            .ThenInclude(c => c.ContactPeople)
                        .Include(o => o.TruckAssignments!.Where(ta => ta.Id == truckAssignmentId))
                            .ThenInclude(ta => ta.Goods)!
                                .ThenInclude(g => g.Container)

                        .Include(o => o.PortOfLoading)
                        .Where(d => d.Id == operationId)
                        .Select(o => new AllDocDto
                        {
                            OperationNumber = o.OperationNumber,
                            PINumber = o.PINumber,
                            PIDate = o.PIDate,
                            // CustomerName = o.Company.ContactPerson.Name,
                            // CustomerAddress = string.Concat(o.Company.ContactPerson.City, " , ", o.Company.ContactPerson.Country),
                            // CustomerPhone = o.Company.ContactPerson.Phone,
                            // CustomerTinNumber = o.Company.ContactPerson.TinNumber,
                            CountryOfOrigin = o.CountryOfOrigin,
                            PortOfLoading = o.PortOfLoading.Country,
                            FinalDestination = o.FinalDestination,
                            Consignee = o.Consignee,
                            PlaceOfDelivery = o.TruckAssignments == null
                                                ? null
                                                : o.TruckAssignments.FirstOrDefault()!.DestinationLocation,
                            TransportationMethod = o.TruckAssignments == null
                                                ? null
                                                : o.TruckAssignments.FirstOrDefault()!.TransportationMethod,
                            Goods = o.Goods!.Select(g => new CIGoodsDto
                            {
                                Description = g.Description,
                                HSCode = g.HSCode,
                                Quantity = g.Quantity,
                                Unit = g.Unit,
                                UnitPrice = g.UnitPrice,
                                CBM = g.CBM,
                                Weight = g.Weight,
                                ContainerNumber = g.Container == null
                                            ? null
                                            : g.Container.ContianerNumber,
                                SealNumber = g.Container == null
                                            ? null
                                            : g.Container.SealNumber
                            })

                        })
                        .FirstOrDefaultAsync();

        if (data == null)
        {
            throw new GhionException(CustomResponse.NotFound($"Operation with id = {operationId} is Not found!"));
        }
        // fetch name on permit/ contact person for the documentation



        if (docType != Documents.Waybill)
        {

            if (data.PINumber == null)
            {
                throw new GhionException(CustomResponse.BadRequest("pi number cannot be null!"));
            }

            var doc = _context.Documentations.Where(d => d.OperationId == operationId && d.Type == Enum.GetName(typeof(Documents), docType)).Select(d => new Documentation
            {
                Date = d.Date,
                PurchaseOrderDate = d.PurchaseOrderDate,
                PurchaseOrderNumber = d.PurchaseOrderNumber,
                PaymentTerm = d.PaymentTerm,
                Fright = d.Fright,
                IsPartialShipmentAllowed = d.IsPartialShipmentAllowed
            }).FirstOrDefault();
            if (doc == null)
            {
                throw new GhionException(CustomResponse.Failed("Documentation Not found!", 450));
            }
            data!.Date = doc.Date;
            data.PurchaseOrderDate = doc.PurchaseOrderDate;
            data.PurchaseOrderNumber = doc.PurchaseOrderNumber;
            data.PaymentTerm = doc.PaymentTerm;
            data.PartialShipment = doc.IsPartialShipmentAllowed == null
                        ? null
                        : (bool)doc.IsPartialShipmentAllowed! ? "TO BE ALLOWED" : "NOT ALLOWED";
        }
        else
        {
            // if waybill
            var truckData = await _context.TruckAssignments
                .Include(ta => ta.Driver)
                .Include(ta => ta.Truck)
                .Where(ta => ta.Id == truckAssignmentId)
                .Select(ta => new
                {
                    DriverName = ta.Driver == null ? null : ta.Driver.Fullname,
                    DriverPhone = ta.Driver == null ? null : ta.Driver.Address.Phone,
                    DriverLicense = ta.Driver == null ? null : ta.Driver.LicenceNumber,
                    TruckNumber = ta.Truck == null ? null : ta.Truck.TruckNumber,
                    PlateNumber = ta.Truck == null ? null : ta.Truck.PlateNumber
                }).FirstOrDefaultAsync();
            data!.DriverName = truckData!.DriverName;
            data.DriverPhone = truckData.DriverPhone;
            data.DriverLicenceNumber = truckData.DriverLicense;
            data.TruckNumber = truckData.TruckNumber;
            data.PlateNumber = truckData.PlateNumber;
            ////////
        }

        if (docType == Documents.CommercialInvoice)
        {
            var defaultInfo = await _context.Settings
            .Include(s => s.DefaultCompany)
            .Include(s => s.DefaultCompany.BankInformation)
            .Select(s => new Setting
            {
                DefaultCompany = new Company
                {
                    BankInformation = s.DefaultCompany.BankInformation.Select(b => new BankInformation
                    {
                        AccountHolderName = b.AccountHolderName,
                        BankName = b.BankName,
                        AccountNumber = b.AccountNumber,
                        SwiftCode = b.SwiftCode,
                        BankAddress = b.BankAddress
                    }).ToList()
                },
            }).FirstOrDefaultAsync();

            if (defaultInfo == null)
            {
                throw new GhionException(CustomResponse.NotFound("ghion bank info not found"));
            }

            data.AccountHolderName = defaultInfo.DefaultCompany.BankInformation.First().AccountHolderName;
            data.BankName = defaultInfo.DefaultCompany.BankInformation.First().BankName;
            data.BankAddress = defaultInfo.DefaultCompany.BankInformation.First().BankAddress;
            data.AccountNumber = defaultInfo.DefaultCompany.BankInformation.First().AccountNumber;
            data.SwiftCode = defaultInfo.DefaultCompany.BankInformation.First().SwiftCode;

        }

        switch (docType)
        {
            case Documents.CommercialInvoice:
            case Documents.ProformaInvoice:
                return _mapper.Map<CommercialInvoiceDto2>(data);
            case Documents.PackageList:
                return _mapper.Map<PackingListDto>(data);
            case Documents.TruckWayBill:
                return _mapper.Map<TruckWayBillDto2>(data);
            case Documents.Waybill:
                return _mapper.Map<WaybillDto>(data);
            default:
                throw new GhionException(CustomResponse.Failed("unknown document type"));

        }

    }
}