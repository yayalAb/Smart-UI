using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Service;
using Application.ContainerModule;
using Application.OperationDocuments.Number9.N9Dtos;
using Application.OperationDocuments.Queries.Number9;
using Application.OperationFollowupModule;
using AutoMapper;
using Domain.Common.PaymentTypes;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.OperationDocuments.Number9Transfer;
public record GenerateTransferNumber9Query : IRequest<TransferNumber9Dto>
{
    public int OperationId { get; set; }
    public int NameOnPermitId { get; set; }
    public int DestinationPortId { get; set; }
    public IEnumerable<int>? ContainerIds { get; set; }
    public IEnumerable<GoodWithQuantityDto>? GoodIds { get; set; }
}
public class GenerateTransferNumber9QueryHandler : IRequestHandler<GenerateTransferNumber9Query, TransferNumber9Dto>
{
    private readonly IAppDbContext _context;
    private readonly OperationEventHandler _operationEventHandler;
    private readonly IMapper _mapper;
    private readonly DefaultCompanyService _defaultCompanyService;
    private readonly ILogger<GenerateTransferNumber9QueryHandler> _logger;
    private readonly GeneratedDocumentService _generatedDocumentService;

    public GenerateTransferNumber9QueryHandler(IAppDbContext context, OperationEventHandler operationEventHandler, IMapper mapper, DefaultCompanyService defaultCompanyService , ILogger<GenerateTransferNumber9QueryHandler> logger , GeneratedDocumentService generatedDocumentService)
    {
        _context = context;
        _operationEventHandler = operationEventHandler;
        _mapper = mapper;
        _defaultCompanyService = defaultCompanyService;
        _logger = logger;
        _generatedDocumentService = generatedDocumentService;
    }
    public async Task<TransferNumber9Dto> Handle(GenerateTransferNumber9Query request, CancellationToken cancellationToken)
    {
        var executionStrategy = _context.database.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(async () =>
        {
            using (var transaction = _context.database.BeginTransaction())
            {
                try
                {
                    // save transferNO.9 doc
                    var createDocRequest = new CreateGeneratedDocDto{
                        OperationId = request.OperationId,
                        NameOnPermitId = request.NameOnPermitId,
                        DestinationPortId = request.DestinationPortId,
                        documentType = Documents.TransferNumber9,
                        ContainerIds = request.ContainerIds,
                        GoodIds = request.GoodIds
                    };
                    var createDocResult = await _generatedDocumentService.CreateGeneratedDocumentRecord(createDocRequest, cancellationToken);

                    // generated document status
                    await _operationEventHandler.DocumentGenerationEventAsync(cancellationToken, new OperationStatus
                    {
                        GeneratedDocumentName = Enum.GetName(typeof(Documents), Documents.TransferNumber9)!,
                        GeneratedDate = DateTime.Now,
                        IsApproved = false,
                        OperationId = request.OperationId
                    }, Enum.GetName(typeof(Status), Status.TransferNumber9Generated)!);

                    // fetch transferNo.9 data
                    var operation = _context.Operations.Where(d => d.Id == request.OperationId)
                              .Include(o => o.Company)
                              .Include(o => o.Goods)
                              .Include(o => o.PortOfLoading)
                              .Include(o => o.Company.ContactPeople)
                              .Select(o => new N9OperationDto
                              {
                                  Id = o.Id,
                                  ShippingLine = o.ShippingLine,
                                  GoodsDescription = o.GoodsDescription,
                                  Quantity = o.Quantity,
                                  GrossWeight = o.GrossWeight,
                                  DestinationType = o.DestinationType,
                                  SourceDocument = o.SourceDocument,
                                  EstimatedTimeOfArrival = o.EstimatedTimeOfArrival,
                                  VoyageNumber = o.VoyageNumber,
                                  OperationNumber = o.OperationNumber,
                                  PortOfLoading = new N9PortOfLoadingDto
                                  {
                                      PortNumber = o.PortOfLoading.PortNumber,
                                      Country = o.PortOfLoading.Country,
                                      Region = o.PortOfLoading.Region,
                                      Vollume = o.PortOfLoading.Vollume
                                  },
                                  CompanyId = o.CompanyId,
                                  SNumber = o.SNumber,
                                  SDate = o.SDate,
                                  VesselName = o.VesselName,
                                  ArrivalDate = o.ArrivalDate,
                                  CountryOfOrigin = o.CountryOfOrigin,
                                  REGTax = o.REGTax,
                                  Localization = o.Localization,
                                  Company = new N9CompanyDto
                                  {
                                      Name = o.Company.Name,
                                      TinNumber = o.Company.TinNumber,
                                      CodeNIF = o.Company.CodeNIF,
                                  },

                              }).First();

                    if (operation == null)
                    {
                        throw new GhionException(CustomResponse.NotFound("Operation Not found!"));
                    }
                    var company = operation.Company;
                    operation.Company = null;

                    //loading the selected name on permit //
                    var nameOnPermit = _mapper.Map<N9NameOnPermitDto>(await _context.ContactPeople.FindAsync(request.NameOnPermitId));
                    company.ContactPerson = nameOnPermit;

                    var payment = _context.Payments.Where(c => c.OperationId == request.OperationId && c.Name == ShippingAgentPaymentType.DeliveryOrder)
                            .Select(p => new N9PaymentDto
                            {
                                PaymentDate = p.PaymentDate,
                                DONumber = p.DONumber
                            }).FirstOrDefault();

                    var companySetting = await _defaultCompanyService.GetDefaultCompanyAsync();


                    await transaction.CommitAsync();
                    return new TransferNumber9Dto
                    {
                        defaultCompanyCodeNIF = companySetting.DefaultCompany.CodeNIF,
                        defaultCompanyName = companySetting.DefaultCompany.Name,
                        company = company,
                        operation = operation,
                        doPayment = payment,
                        goods = _mapper.Map<List<N9GoodDto>>(createDocResult.goods),
                        containers = _mapper.Map<List<ContainerDto>>(createDocResult.containers)
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        });
    }
    // private async Task<(List<Good> goods, List<Container> containers)> CreateGeneratedDocumentRecord(GenerateTransferNumber9Query request, CancellationToken cancellationToken)
    // {
    //     /// method two
    //     //    var goods =  _context.Goods
    //     //         .Where(g => request.GoodIds.ToList()
    //     //             .Where(gi => gi.Id == g.Id).Any()
    //     //         ).ToList();
    //     //     goods.ForEach(good => {
    //     //         var remaining = good.RemainingQuantity - request.GoodIds.Where(gi => gi.Id == good.Id).FirstOrDefault().Quantity;
    //     //     });
    //     ////---------------
    //     List<Good> goods = new List<Good>();
    //     List<Container> containers = new List<Container>();
    //     if (request.ContainerIds == null && request.GoodIds == null)
    //     {
    //         throw new GhionException(CustomResponse.BadRequest("both goodIds and containerIds can not be null!"));
    //     }
    //     // if unstaff goods 
    //     if (request.GoodIds != null)
    //     {  
    //         var newGeneratedDoc = new GeneratedDocument
    //         {
    //             LoadType = "good",
    //             DocumentType = Enum.GetName(typeof(Documents), Documents.TransferNumber9)!,
    //             OperationId = request.OperationId,
    //             DestinationPortId = request.DestinationPortId,
    //             ContactPersonId = request.NameOnPermitId,

    //         };
    //         await _context.GeneratedDocuments.AddAsync(newGeneratedDoc);
    //         await _context.SaveChangesAsync(cancellationToken);
    //         for (int i = 0; i < request.GoodIds.Count(); i++)
    //         {
    //             _logger.LogCritical("test");
    //             var good = await _context.Goods.Where(g => g.Id  == request.GoodIds.ToList()[i].Id).FirstOrDefaultAsync();
    //             if (good == null)
    //             {
    //                 throw new GhionException(CustomResponse.NotFound($"good with id {request.GoodIds.ToList()[i].Id} is not found!"));
    //             }
    //             var remainingAmount = good.RemainingQuantity - request.GoodIds.ToList()[i].Quantity;
    //             if (remainingAmount < 0)
    //             {
    //                 throw new GhionException(CustomResponse.BadRequest($"the entered quantity for good with id {request.GoodIds.ToList()[i].Id} is excess to the remaining amount of the good!"));
    //             }
    //             good.RemainingQuantity = remainingAmount;
    //             _context.Goods.Update(good);
    //             await _context.SaveChangesAsync(cancellationToken);
    //             goods.Add(good);
    //             var newGeneratedDocGood = new GeneratedDocumentGood
    //             {
    //                 Quantity = request.GoodIds.ToList()[i].Quantity,
    //                 GoodId = good.Id,
    //                 GeneratedDocumentId = newGeneratedDoc.Id,
    //             };
    //             await _context.GeneratedDocumentsGoods.AddAsync(newGeneratedDocGood);
    //             await _context.SaveChangesAsync(cancellationToken);


    //         }
    //     }
    //     //if contained goods
    //     if (request.ContainerIds != null)
    //     {

    //         containers = _context.Containers
    //             .Where(g => request.ContainerIds.ToList().Contains(g.Id)).ToList();
    //         if (containers.Count != request.ContainerIds.ToList().Count)
    //         {
    //             var unfoundIds = request.ContainerIds.ToList()
    //                     .Where(id => containers
    //                         .Where(c => c.Id == id).Any()
    //                     ).ToList();
    //             throw new GhionException(CustomResponse.BadRequest($" containers with ids = {unfoundIds} are not found "));
    //         }

    //         var newGeneratedDoc = new GeneratedDocument
    //         {
    //             LoadType = "container",
    //             DocumentType = Enum.GetName(typeof(Documents), Documents.TransferNumber9)!,
    //             OperationId = request.OperationId,
    //             DestinationPortId = request.DestinationPortId,
    //             ContactPersonId = request.NameOnPermitId,
    //             Containers = containers
    //         };

    //         await _context.GeneratedDocuments.AddAsync(newGeneratedDoc);
    //         await _context.SaveChangesAsync(cancellationToken);

    //     }
    //     return (goods: goods, containers: containers);
    // }


}
