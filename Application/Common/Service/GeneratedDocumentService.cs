using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Service;
public class GeneratedDocumentService {
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GeneratedDocumentService(IAppDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
      public async Task<(List<Good> goods, List<Container> containers)> CreateGeneratedDocumentRecord(CreateGeneratedDocDto request, CancellationToken cancellationToken)
    {
        List<Good> goods = new List<Good>();
        List<Container> containers = new List<Container>();
        if (request.ContainerIds == null && request.GoodIds == null)
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
                    GeneratedDocumentId = newGeneratedDoc.Id,
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
                DocumentType = Enum.GetName(typeof(Documents), request.documentType )!,
                OperationId = request.OperationId,
                DestinationPortId = request.DestinationPortId,
                ContactPersonId = request.NameOnPermitId,
                Containers = containers
            };

            await _context.GeneratedDocuments.AddAsync(newGeneratedDoc);
            await _context.SaveChangesAsync(cancellationToken);

        }
        return (goods: goods, containers: containers);
    }
    public async Task<GeneratedDocumentDto> fetchGeneratedDocument(int id , CancellationToken cancellationToken){
        return await _context.GeneratedDocuments
                .Include(gd =>gd.Operation)
                .Include(gd =>gd.DestinationPort)
                .Include(gd =>gd.ContactPerson)
                .Include(gd =>gd.Containers)
                .Include(gd =>gd.GeneratedDocumentsGoods)
                .Where(gd => gd.Id == id).ProjectTo<GeneratedDocumentDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
    }


}