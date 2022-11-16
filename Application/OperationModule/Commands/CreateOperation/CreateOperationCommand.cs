
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.OperationModule.Commands.CreateOperation
{
    public record CreateOperationCommand : IRequest<int>
    {
        public string CustomerName { get; init; }
        public string? NameOnPermit { get; init; }
        public string? Consignee { get; init; }
        public string NotifyParty { get; init; }
        public string BillNumber { get; init; }
        public string ShippingLine { get; init; }
        public string GoodsDescription { get; init; }
        public float Quantity { get; init; }
        public float? GrossWeight { get; init; }
        public string? ATA { get; init; }
        public string? FZIN { get; init; }
        public string? FZOUT { get; init; }
        public string DestinationType { get; init; }
        public IFormFile? SourceDocument { get; init; }
        public string SourceDocumentType { get; init; }
        public DateTime? ActualDateOfDeparture { get; init; }
        public DateTime? EstimatedTimeOfArrival { get; init; }
        public string VoyageNumber { get; init; }
        public string TypeOfMerchandise { get; init; }
        public string OperationNumber { get; init; } = null!;
        public DateTime OpenedDate { get; init; }
        public string Status { get; init; } = null!;
        public IFormFile? ECDDocument { get; init; }
        public int ShippingAgentId { get; init; }
        public int PortOfLoadingId { get; init; }
        public int CompanyId { get; init; }
    }
    public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, int>
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
        public async Task<int> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
        {
             var executionStrategy = _context.database.CreateExecutionStrategy();
             return await executionStrategy.ExecuteAsync(async () =>{
                 using(var transaction = _context.database.BeginTransaction()){
                    byte[]? ecdDoc = null;
                    byte[]? sourceDoc = null;
                    try
                    {
                        if(request.ECDDocument != null){
                        var ECDresponse = await _fileUploadService.GetFileByte(request.ECDDocument , FileType.EcdDocument);
                        if(!ECDresponse.result.Succeeded){
                            throw new Exception(String.Join(" , ", ECDresponse.result.Errors)); 
                        }
                        ecdDoc = ECDresponse.byteData;
                        }
                        if(request.SourceDocument != null){
                            var sourceResponse = await _fileUploadService.GetFileByte(request.ECDDocument , FileType.SourceDocument);
                            if(!sourceResponse.result.Succeeded){
                                throw new Exception(String.Join(" , ", sourceResponse.result.Errors)); 
                            }
                            sourceDoc = sourceResponse.byteData;
                        }
                        Operation newOperation = _mapper.Map<Operation>(request);
                        newOperation.ECDDocument = ecdDoc;
                        newOperation.SourceDocument = sourceDoc;
                        await _context.Operations.AddAsync(newOperation);
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync();
                        return newOperation.Id;
                    }
                    catch (System.Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                   
                 


                 }
             });
          
        }
    }
}
