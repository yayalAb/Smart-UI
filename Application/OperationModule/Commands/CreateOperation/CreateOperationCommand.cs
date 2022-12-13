
using System.Reflection.Metadata;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.OperationModule.Commands.CreateOperation
{
    public record CreateOperationCommand : IRequest<object>
    {
        public string BillNumber { get; set; }
        public int ContactPersonId { get; set; }
        public string DestinationType { get; set; }
        public int CompanyId { get; set; }
        public int PortOfLoadingId { get; set; }
    }
    public class CreateOperationCommandHandler : IRequestHandler<CreateOperationCommand, object>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        private readonly IFileUploadService _fileUploadService;
        private readonly OperationService _operationService;
        private readonly ILogger<CreateOperationCommandHandler> _logger;

        public CreateOperationCommandHandler(IAppDbContext context, IMapper mapper, IFileUploadService fileUploadService,OperationService operationService, ILogger<CreateOperationCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
            _operationService = operationService;
            _logger = logger;
        }
        public async Task<object> Handle(CreateOperationCommand request, CancellationToken cancellationToken)
        {

            var executionStrategy = _context.database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.database.BeginTransaction())
                {
                    try
                    {
                        //create operation with empty operation number
                        Operation newOperation = _mapper.Map<Operation>(request);
                        newOperation.OperationNumber = "";
                        newOperation.Status = Enum.GetName(typeof(Status), Status.Opened)!;
                        newOperation.OpenedDate = DateTime.Now;
                        await _context.Operations.AddAsync(newOperation);
                        await _context.SaveChangesAsync(cancellationToken);

                        // update operation number to a unique value
                        newOperation.OperationNumber = _operationService.GenerateOperationNumber(newOperation.Id, request.DestinationType);
                        _context.Operations.Update(newOperation);
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync();

                        return new {Id = newOperation.Id , operationNumber = newOperation.OperationNumber};
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
