
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.ShippingAgentModule.Commands.CreateShippingAgent;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.ShippingAgentModule.Commands.UpdateShippingAgent
{
    public record UpdateShippingAgentCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? CompanyName { get; set; }
        public int ImageId { get; set; }
        public int AddressId { get; set; }
        public IFormFile? ImageFile { get; set; }
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
            using var transaction = _context.database.BeginTransaction();
            try
            {
                /// update image 
                if (request.ImageFile != null)
                {
                    var response = await _fileUploadService.updateFile(request.ImageFile, FileType.Image,request.ImageId);
                    if (!response.Succeeded)
                    {
                        throw new CustomBadRequestException(String.Join(" , ", response.Errors));
                    }
                   
                }
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
                var oldShippingAgent = await _context.ShippingAgents.FindAsync(request.Id);
                if (oldShippingAgent == null)
                {
                    throw new NotFoundException("shippingAgnet", new { Id = request.Id });
                }
                oldShippingAgent.FullName = request.FullName;
                oldShippingAgent.CompanyName = request.CompanyName; 
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
    }

}
