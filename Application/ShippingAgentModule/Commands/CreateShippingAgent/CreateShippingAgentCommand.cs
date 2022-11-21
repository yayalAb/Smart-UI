

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.ShippingAgentModule.Commands.CreateShippingAgent
{
    public record CreateShippingAgentCommand : IRequest<int>
    {
        public string FullName { get; set; } = null!;
        public string? CompanyName { get; set; }
        public byte[]? Image { get; set; }
        public AddressDto Address { get; set; }
    }
    public class CreateShippingAgentCommandHandler : IRequestHandler<CreateShippingAgentCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IFileUploadService _fileUploadService;
        private readonly IMapper _mapper;

        public CreateShippingAgentCommandHandler(IAppDbContext context , IFileUploadService fileUploadService  , IMapper mapper)
        {
            _context = context;
            _fileUploadService = fileUploadService;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateShippingAgentCommand request, CancellationToken cancellationToken)
        {
          var executionStrategy = _context.database.CreateExecutionStrategy();
         return  await executionStrategy.ExecuteAsync(
            async ()=>{
                 using (var transaction = _context.database.BeginTransaction()){

            try
            {
                // byte[]? imageByte = null; 
                // /// save image to db and retrive id
                //  if(request.ImageFile != null)
                // {
                //     var response = await _fileUploadService.GetFileByte(request.ImageFile, FileType.Image);
                //     if (!response.result.Succeeded)
                //     {
                //         throw new CustomBadRequestException(String.Join(" , ", response.result.Errors));
                //     }
                //      imageByte = response.byteData;
                // }
                /// save address to db 
                Address address = _mapper.Map<Address>(request.Address);    
                await _context.Addresses.AddAsync(address);
                await _context.SaveChangesAsync(cancellationToken);

                // save shipping agent 
                ShippingAgent shippingAgent = new ShippingAgent
                {
                    FullName = request.FullName,
                    CompanyName = request.CompanyName,
                    AddressId = address.Id,
                    Image = request.Image 
                };
                await _context.ShippingAgents.AddAsync(shippingAgent);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync();

                return shippingAgent.Id;


            }
            catch (Exception)
            {
                await transaction.RollbackAsync();  
                throw;
            }
        }
          });
       
           }
    }

}
