using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.OperationDocuments.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Service;
public class GeneratedDocumentService
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GeneratedDocumentService(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<int> CreateGeneratedDocumentRecord(CreateGeneratedDocDto request, CancellationToken cancellationToken)
    {
        List<Good> goods = new List<Good>();
        List<Container> containers = new List<Container>();
        int generatedDocumentId = 0;
        if ((request.ContainerIds == null || request.ContainerIds.Count() == 0) && (request.GoodIds == null || request.GoodIds.Count() == 0))
        {
            throw new GhionException(CustomResponse.BadRequest("both goodIds and containerIds can not be null!"));
        }
        // if unstaff goods 
        if (request.GoodIds != null)
        {
            var newGeneratedDoc = new GeneratedDocument
            {
                LoadType = "good",
                DocumentType = Enum.GetName(typeof(Documents), Documents.TransferNumber9)!,
                OperationId = request.OperationId,
                DestinationPortId = request.DestinationPortId,
                ContactPersonId = request.NameOnPermitId,

            };
            await _context.GeneratedDocuments.AddAsync(newGeneratedDoc);
            await _context.SaveChangesAsync(cancellationToken);
            generatedDocumentId = newGeneratedDoc.Id;
            for (int i = 0; i < request.GoodIds.Count(); i++)
            {

                var good = await _context.Goods.FindAsync(request.GoodIds.ToList()[i].Id);
                if (good == null)
                {
                    throw new GhionException(CustomResponse.NotFound($"good with id {request.GoodIds.ToList()[i].Id} is not found!"));
                }
                var remainingAmount = good.RemainingQuantity - request.GoodIds.ToList()[i].Quantity;
                if (remainingAmount < 0)
                {
                    throw new GhionException(CustomResponse.BadRequest($"the entered quantity for good with id {request.GoodIds.ToList()[i].Id} is excess to the remaining amount of the good!"));
                }
                good.RemainingQuantity = remainingAmount;
                _context.Goods.Update(good);
                await _context.SaveChangesAsync(cancellationToken);
                goods.Add(good);
                var newGeneratedDocGood = new GeneratedDocumentGood
                {
                    Quantity = request.GoodIds.ToList()[i].Quantity,
                    GoodId = good.Id,
                    GeneratedDocumentId = generatedDocumentId,
                };

                await _context.GeneratedDocumentsGoods.AddAsync(newGeneratedDocGood);
                await _context.SaveChangesAsync(cancellationToken);


            }
        }
        //if contained goods
        if (request.ContainerIds != null)
        {

            containers = _context.Containers
                .Where(g => request.ContainerIds.ToList().Contains(g.Id)).ToList();
            if (containers.Count != request.ContainerIds.ToList().Count)
            {
                var unfoundIds = request.ContainerIds.ToList()
                        .Where(id => containers
                            .Where(c => c.Id == id).Any()
                        ).ToList();
                throw new GhionException(CustomResponse.BadRequest($" containers with ids = {unfoundIds} are not found "));
            }

            var newGeneratedDoc = new GeneratedDocument
            {
                LoadType = "container",
                DocumentType = Enum.GetName(typeof(Documents), request.documentType)!,
                OperationId = request.OperationId,
                DestinationPortId = request.DestinationPortId,
                ContactPersonId = request.NameOnPermitId,
                Containers = containers
            };

            await _context.GeneratedDocuments.AddAsync(newGeneratedDoc);
            await _context.SaveChangesAsync(cancellationToken);
            generatedDocumentId = newGeneratedDoc.Id;


        }
        var destinationPort = await _context.Ports.FindAsync(request.DestinationPortId);
        return generatedDocumentId;
    }
    public async Task<GeneratedDocumentDto> fetchGeneratedDocument(int id, CancellationToken cancellationToken)
    {
        var doc = await _context.GeneratedDocuments
                .Include(gd => gd.Operation)
                .Include(gd => gd.DestinationPort)
                .Include(gd => gd.ContactPerson)
                .Include(gd => gd.Containers)
                .Include(gd => gd.GeneratedDocumentsGoods)
                .Where(gd => gd.Id == id)
                // .ProjectTo<GeneratedDocumentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        if(doc == null){
            throw new GhionException(CustomResponse.NotFound($"generatedDocument with id = {id} is not found"));
        }
        return new GeneratedDocumentDto
        {
            LoadType = doc.LoadType,
            DocumentType = doc.DocumentType,
            Operation = doc. Operation,
            DestinationPort = doc.DestinationPort,
            ContactPerson = doc.ContactPerson,
            Containers = doc.Containers.ToList(),
            Goods =  doc.GeneratedDocumentsGoods.Select(gdg => new DocGoodDto
                    {
                        Id = gdg.Good.Id,
                        Description = gdg.Good.Description,
                        HSCode = gdg.Good.HSCode,
                        Manufacturer = gdg.Good.Manufacturer,
                        Weight = (gdg.Good.Weight * gdg.Quantity) / gdg.Good.Quantity,// (total good weight * quantity for transfer9 /total good quantity)
                        WeightUnit = gdg.Good.WeightUnit,
                        Quantity = gdg.Quantity, // the selected quantity for transfer9
                        InitialQuantity = gdg.Good.Quantity, // quanitity entere`
                        RemainingQuantity = gdg.Good.RemainingQuantity,
                        Type = gdg.Good.Type,
                        Location = gdg.Good.Location,
                        ChasisNumber = gdg.Good.ChasisNumber,
                        EngineNumber = gdg.Good.EngineNumber,
                        ModelCode = gdg.Good.ModelCode,
                        Unit = gdg.Good.Unit,
                        UnitPrice = gdg.Good.UnitPrice,
                        CBM = gdg.Good.CBM
                    }).ToList()

        };

    }


}