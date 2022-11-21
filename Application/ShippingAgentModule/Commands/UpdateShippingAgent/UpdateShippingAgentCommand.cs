
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.ShippingAgentModule.Commands.UpdateShippingAgent
{
    public record UpdateShippingAgentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? CompanyName { get; set; }
        public int AddressId { get; set; }
        public byte[]? Image { get; set; }
        public AddressDto Address { get; set; }

    }
    public class UpdateShippingAgentCommandHandler : IRequestHandler<UpdateShippingAgentCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IFileUploadService _fileUploadService;
        private readonly IMapper _mapper;

        public UpdateShippingAgentCommandHandler(IAppDbContext context , IFileUploadService fileUploadService , IMapper mapper)
        {
            _context = context;
            _fileUploadService = fileUploadService;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateShippingAgentCommand request, CancellationToken cancellationToken)
        {
            
            var executionStrategy = _context.database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(async()=>{

            using( var transaction = _context.database.BeginTransaction()){
            try
            {
                /// check if shipping agent exists
                var oldShippingAgent = await _context.ShippingAgents.FindAsync(request.Id);
                if (oldShippingAgent == null)
                {
                    throw new NotFoundException("shippingAgnet", new { Id = request.Id });
                }


                // /// update image 
                // byte[]? newImage = oldShippingAgent.Image;
                // if (request.ImageFile != null)
                // {
                //     var response = await _fileUploadService.GetFileByte(request.ImageFile , FileType.Image);
                //     if (!response.result.Succeeded)
                //     {
                //         throw new CustomBadRequestException(String.Join(" , ", response.result.Errors));
                //     }
                //     newImage = response.byteData;
                   
                // }
                /// update address  
                var newAddress = request.Address;
                var oldAddress = await _context.Addresses.FindAsync(request.AddressId);
                if(oldAddress == null)
                {
                    throw new NotFoundException("Address" , new {Id=request.AddressId});
                }

                oldAddress.Email =newAddress.Email;
                oldAddress.City =newAddress.City;
                oldAddress.Subcity = newAddress.Subcity;
                oldAddress.Country =newAddress.Country; 
                oldAddress.Phone = newAddress.Phone;
                oldAddress.POBOX = newAddress.POBOX;

                _context.Addresses.Update(oldAddress);
                await _context.SaveChangesAsync(cancellationToken);

                // update shipping agent 
              
                oldShippingAgent.FullName = request.FullName;
                oldShippingAgent.CompanyName = request.CompanyName;
                oldShippingAgent.Image = request.Image;
                 _context.ShippingAgents.Update(oldShippingAgent);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync();

                return oldShippingAgent.Id;


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
