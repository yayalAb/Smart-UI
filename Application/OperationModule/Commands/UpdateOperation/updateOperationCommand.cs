
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationModule.Commands.UpdateOperation

{
    public record UpdateOperationCommand : IRequest<CustomResponse>
    {
        public int Id { get; set; }
        public string? NameOnPermit { get; set; }
        public string? Consignee { get; set; }
        public string? NotifyParty { get; set; }
        public string? BillNumber { get; set; }
        public string? ShippingLine { get; set; }
        public string? GoodsDescription { get; set; }
        public float Quantity { get; set; }
        public float? GrossWeight { get; set; }
        public string? ATA { get; set; }
        public string? FZIN { get; set; }
        public string? FZOUT { get; set; }
        public string? DestinationType { get; set; }
        public byte[]? SourceDocument { get; set; }
        public DateTime? ActualDateOfDeparture { get; set; }
        public DateTime? EstimatedTimeOfArrival { get; set; }
        public string? VoyageNumber { get; set; }
        public string? TypeOfMerchandise { get; set; }
        public string OperationNumber { get; set; } = null!;
        public DateTime OpenedDate { get; set; }
        public string Status { get; set; } = null!;
        public byte[]? ECDDocument { get; set; }
        public int? ShippingAgentId { get; set; }
        public int? PortOfLoadingId { get; set; }
        public int CompanyId { get; set; }
        /////------------Additionals------
        public string? SNumber { get; set; }
        public DateTime? SDate { get; set; }
        public string? RecepientName { get; set; }
        public string? VesselName { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string? ConnaissementNumber { get; set; }
        public string? CountryOfOrigin { get; set; }
        public float? REGTax { get; set; }
        public string? BillOfLoadingNumber { get; set; }

        //--------------------------------------//
    }
    public class UpdateOperationCommandHandler : IRequestHandler<UpdateOperationCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUploadService;

        public UpdateOperationCommandHandler(IAppDbContext context, IMapper mapper, IFileUploadService fileUploadService)
        {
            _context = context;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
        }
        public async Task<CustomResponse> Handle(UpdateOperationCommand request, CancellationToken cancellationToken)
        {
            var existingOperation = await _context.Operations.FindAsync(request.Id);
            if (!_context.Operations.Any(o => o.Id == request.Id))
            {
                throw new GhionException(CustomResponse.NotFound($"operation with Id = {request.Id} is not found"));
            }
            Operation updatedOperation = _mapper.Map<Operation>(request);
            _context.Operations.Update(updatedOperation);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("operation updated successfully!");


            //   var executionStrategy = _context.database.CreateExecutionStrategy();
            //      return await executionStrategy.ExecuteAsync(async () =>{
            //          using(var transaction = _context.database.BeginTransaction()){

            //             try
            //             {
            //                 var existingOperation = await _context.Operations.FindAsync(request.Id);
            //                 if(existingOperation == null){
            //                     throw new NotFoundException("operation" , new{ Id = request.Id});
            //                 }



            //                 if(request.ECDDocument != null){
            //                 var ECDresponse = await _fileUploadService.GetFileByte(request.ECDDocument , FileType.EcdDocument);
            //                     if(!ECDresponse.result.Succeeded){
            //                         throw new Exception(String.Join(" , ", ECDresponse.result.Errors)); 
            //                     }
            //                     existingOperation.ECDDocument = ECDresponse.byteData;
            //                 }
            //                 if(request.SourceDocument != null){
            //                     var sourceResponse = await _fileUploadService.GetFileByte(request.SourceDocument , FileType.SourceDocument);
            //                     if(!sourceResponse.result.Succeeded){
            //                         throw new Exception(String.Join(" , ", sourceResponse.result.Errors)); 
            //                     }
            //                     existingOperation.SourceDocument = sourceResponse.byteData;
            //                 }
            //                 existingOperation.NameOnPermit = request.NameOnPermit;
            //                 existingOperation.Consignee = request.Consignee;
            //                 existingOperation.NotifyParty = request.NotifyParty;
            //                 existingOperation.BillNumber = request.BillNumber;
            //                 existingOperation.ShippingLine = request.ShippingLine;
            //                 existingOperation.GoodsDescription = request.GoodsDescription;
            //                 existingOperation.Quantity = request.Quantity;
            //                 existingOperation.GrossWeight = request.GrossWeight;
            //                 existingOperation.ATA = request.ATA;
            //                 existingOperation.FZIN = request.FZIN;
            //                 existingOperation.FZOUT = request.FZOUT;
            //                 existingOperation.DestinationType = request.DestinationType;
            //                 existingOperation.ActualDateOfDeparture = request.ActualDateOfDeparture;
            //                 existingOperation.EstimatedTimeOfArrival = request.EstimatedTimeOfArrival;
            //                 existingOperation.VoyageNumber = request.VoyageNumber;
            //                 existingOperation.TypeOfMerchandise = request.TypeOfMerchandise;
            //                 existingOperation.OperationNumber = request.OperationNumber;
            //                 existingOperation.OpenedDate = request.OpenedDate;
            //                 existingOperation.Status = request.Status;
            //                 existingOperation.ShippingAgentId = request.ShippingAgentId;
            //                 existingOperation.PortOfLoadingId = request.PortOfLoadingId;
            //                 existingOperation.CompanyId = request.CompanyId;


            //                 _context.Operations.Update(existingOperation);
            //                 await _context.SaveChangesAsync(cancellationToken);
            //                 await transaction.CommitAsync();
            //                 return existingOperation.Id;
            //             }
            //             catch (System.Exception)
            //             {
            //                 await transaction.RollbackAsync();
            //                 throw;
            //             }




            //          }
            //      });

        }
    }
}
