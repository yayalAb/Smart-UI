

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.BillOfLoadingModule.Commands.UpdateBillOfLoading
{
    public record UpdateBillOfLoadingCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string? NameOnPermit { get; set; }
        public string? Consignee { get; set; }
        public string NotifyParty { get; set; }
        public string BillNumber { get; set; }
        public string ShippingLine { get; set; }
        public string GoodsDescription { get; set; }
        public float Quantity { get; set; }
        public int ContainerId { get; set; }
        public float? GrossWeight { get; set; }
        public string? ATA { get; set; }
        public string? FZIN { get; set; }
        public string? FZOUT { get; set; }
        public string DestinationType { get; set; }
        public int ShippingAgentId { get; set; }
        public int PortOfLoadingId { get; set; }
        public DateTime? ActualDateOfDeparture { get; set; }
        public DateTime? EstimatedTimeOfArrival { get; set; }
        public string VoyageNumber { get; set; }
        public string TypeOfMerchandise { get; set; }
        public IFormFile? BillOfLoadingDocument { get; set; }
    }
    public class UpdateBillOfLoadingCommandHandler : IRequestHandler<UpdateBillOfLoadingCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IFileUploadService _fileUploadService;
        public UpdateBillOfLoadingCommandHandler(IAppDbContext context, IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }
        public async Task<int> Handle(UpdateBillOfLoadingCommand request, CancellationToken cancellationToken)
        {
            using var transaction = _context.database.BeginTransaction();
            try
            {
                

                var oldBillOfLoading = await _context.BillOfLoadings.FindAsync(request.Id);
                if(oldBillOfLoading == null)
                {
                    throw new NotFoundException("BillOfLoading", new { Id = request.Id });
                }
                byte[]? newBillOfLoadingDoc = oldBillOfLoading.BillOfLoadingDocument;
                if (request.BillOfLoadingDocument != null)
                {
                    var response = await _fileUploadService.GetFileByte(request.BillOfLoadingDocument, FileType.BillOfLoadingDocument);
                    if (!response.result.Succeeded)
                    {
                        throw new CustomBadRequestException(String.Join(" , ", response.result.Errors));
                    }
                    newBillOfLoadingDoc = response.byteData;
                }
                oldBillOfLoading.CustomerName = request.CustomerName;
                oldBillOfLoading.NameOnPermit = request.NameOnPermit;
                oldBillOfLoading.Consignee = request.Consignee;
                oldBillOfLoading.NotifyParty = request.NotifyParty;
                oldBillOfLoading.BillNumber = request.BillNumber;
                oldBillOfLoading.ShippingLine = request.ShippingLine;
                oldBillOfLoading.GoodsDescription = request.GoodsDescription;
                oldBillOfLoading.Quantity = request.Quantity;
                oldBillOfLoading.ContainerId = request.ContainerId;
                oldBillOfLoading.GrossWeight = request.GrossWeight;
                oldBillOfLoading.ATA = request.ATA;
                oldBillOfLoading.FZIN = request.FZIN;
                oldBillOfLoading.FZOUT = request.FZOUT;
                oldBillOfLoading.DestinationType = request.DestinationType;
                oldBillOfLoading.ShippingAgentId = request.ShippingAgentId;
                oldBillOfLoading.PortOfLoadingId = request.PortOfLoadingId;
                oldBillOfLoading.ActualDateOfDeparture = request.ActualDateOfDeparture;
                oldBillOfLoading.EstimatedTimeOfArrival = request.EstimatedTimeOfArrival;
                oldBillOfLoading.VoyageNumber = request.VoyageNumber;
                oldBillOfLoading.TypeOfMerchandise = request.TypeOfMerchandise;
                oldBillOfLoading.BillOfLoadingDocument = newBillOfLoadingDoc;



                _context.BillOfLoadings.Update(oldBillOfLoading);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return oldBillOfLoading.Id;

            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }




        }
    }
}
