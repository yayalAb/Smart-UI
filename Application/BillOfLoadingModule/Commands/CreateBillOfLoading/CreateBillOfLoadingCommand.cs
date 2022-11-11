
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.BillOfLoadingModule.Commands.CreateBillOfLoading
{
    public record CreateBillOfLoadingCommand : IRequest<int>
    {
        public string CustomerName { get; init; }
        public string? NameOnPermit { get; init; }
        public string? Consignee { get; init; }
        public string NotifyParty { get; init; }
        public string BillNumber { get; init; }
        public string ShippingLine { get; init; }
        public string GoodsDescription { get; init; }
        public float Quantity { get; init; }
        public int ContainerId { get; init; }
        public float? GrossWeight { get; init; }
        public string? ATA { get; init; }
        public string? FZIN { get; init; }
        public string? FZOUT { get; init; }
        public string DestinationType { get; init; }
        public int ShippingAgentId { get; init; }
        public int PortOfLoadingId { get; init; }
        public DateTime? ActualDateOfDeparture { get; init; }
        public DateTime? EstimatedTimeOfArrival { get; init; }
        public string VoyageNumber { get; init; }
        public string TypeOfMerchandise { get; init; }
        public IFormFile? BillOfLoadingDocument { get; init; }

    }
    public class CreateBillOfLoadingCommandHandler : IRequestHandler<CreateBillOfLoadingCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IFileUploadService _fileUploadService;

        public CreateBillOfLoadingCommandHandler(IAppDbContext context , IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }
        public async Task<int> Handle(CreateBillOfLoadingCommand request, CancellationToken cancellationToken)
        {
            using var transaction = _context.database.BeginTransaction();
            try
            {
                byte[]? documentData = null;
                if (request.BillOfLoadingDocument != null)
                {
                    var response = await _fileUploadService.GetFileByte(request.BillOfLoadingDocument, FileType.BillOfLoadingDocument);
                    if (!response.result.Succeeded)
                    {
                        throw new CustomBadRequestException(String.Join(" , ", response.result.Errors));
                    }
                    documentData = response.byteData;
                }
                BillOfLoading newBillOfLoading = new BillOfLoading
                {
                    CustomerName = request.CustomerName,
                    NameOnPermit = request.NameOnPermit,    
                    Consignee = request.Consignee,  
                    NotifyParty = request.NotifyParty,  
                    BillNumber = request.BillNumber,
                    ShippingLine = request.ShippingLine,    
                    GoodsDescription = request.GoodsDescription,
                    Quantity = request.Quantity,    
                    ContainerId = request.ContainerId,  
                    GrossWeight = request.GrossWeight,  
                    ATA = request.ATA,
                    FZIN = request.FZIN,
                    FZOUT = request.FZOUT,
                    DestinationType = request.DestinationType,  
                    ShippingAgentId = request.ShippingAgentId,  
                    PortOfLoadingId = request.PortOfLoadingId,  
                    ActualDateOfDeparture = request.ActualDateOfDeparture,  
                    EstimatedTimeOfArrival = request.EstimatedTimeOfArrival,    
                    VoyageNumber = request.VoyageNumber,
                    TypeOfMerchandise = request.TypeOfMerchandise,  
                    BillOfLoadingDocument = documentData,    
                  
                };
                await _context.BillOfLoadings.AddAsync(newBillOfLoading);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return newBillOfLoading.Id;

            }
            catch (Exception)
            {
               await transaction.RollbackAsync(); 
                throw;
            }
           
              


        }
    }
}
