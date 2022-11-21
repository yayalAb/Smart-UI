
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationModule.Commands.CreateOperation
{
    public record CreateOperationCommand : IRequest<CustomResponse>
    {
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
    }
    public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUploadService;

        public CreateOperationCommandHandler(IAppDbContext context , IMapper mapper , IFileUploadService fileUploadService)
        {
            _context = context;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
        }
        public async Task<CustomResponse> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
        {

            Operation newOperation = _mapper.Map<Operation>(request);
            await _context.Operations.AddAsync(newOperation);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("operation created successfully",201);
            //  var executionStrategy = _context.database.CreateExecutionStrategy();
            //  return await executionStrategy.ExecuteAsync(async () =>{
            //      using(var transaction = _context.database.BeginTransaction()){
            //         byte[]? ecdDoc = null;
            //         byte[]? sourceDoc = null;
            //         try
            //         {
            //             if(request.ECDDocument != null){
            //             var ECDresponse = await _fileUploadService.GetFileByte(request.ECDDocument , FileType.EcdDocument);
            //             if(!ECDresponse.result.Succeeded){
            //                 throw new Exception(String.Join(" , ", ECDresponse.result.Errors)); 
            //             }
            //             ecdDoc = ECDresponse.byteData;
            //             }
            //             if(request.SourceDocument != null){
            //                 var sourceResponse = await _fileUploadService.GetFileByte(request.SourceDocument , FileType.SourceDocument);
            //                 if(!sourceResponse.result.Succeeded){
            //                     throw new Exception(String.Join(" , ", sourceResponse.result.Errors)); 
            //                 }
            //                 sourceDoc = sourceResponse.byteData;
            //             }
            //             Operation newOperation = _mapper.Map<Operation>(request);
            //             newOperation.ECDDocument = ecdDoc;
            //             newOperation.SourceDocument = sourceDoc;
            //             await _context.Operations.AddAsync(newOperation);
            //             await _context.SaveChangesAsync(cancellationToken);
            //             await transaction.CommitAsync();
            //             return newOperation.Id;
            //         }
            //         catch (System.Exception)
            //         {
            //             await transaction.RollbackAsync();
            //             throw;
            //         }
                   
                 


            //      }
            //  });
          
        }
    }
}
