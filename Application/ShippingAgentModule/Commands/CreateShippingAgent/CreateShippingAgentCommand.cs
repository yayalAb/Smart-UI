

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.ShippingAgentModule.Commands.CreateShippingAgent
{
    public record CreateShippingAgentCommand : IRequest<int>
    {
        public string FullName { get; set; } = null!;
        public string? CompanyName { get; set; }
        public IFormFile? ImageFile { get; set; }
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
          
           using var transaction = _context.database.BeginTransaction();
            try
            {
                var imageId = 0; 
                /// save image to db and retrive id
                 if(request.ImageFile != null)
                {
                    var response = await _fileUploadService.uploadFile(request.ImageFile, FileType.Image);
                    if (!response.result.Succeeded)
                    {
                        throw new CustomBadRequestException(String.Join(" , ", response.result.Errors));
                    }
                     imageId = response.Id;
                }
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
                    ImageId = imageId!=0 ? imageId : null, 
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
    }

}
