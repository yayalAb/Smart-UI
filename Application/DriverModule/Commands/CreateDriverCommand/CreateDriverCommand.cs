using MediatR;
using Domain.Entities;
using Domain.Enums;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Application.AddressModule.Commands.AddressCreateCommand;
using Microsoft.AspNetCore.Http;

namespace Application.DriverModule.Commands.CreateDriverCommand
{
    public record CreateDriverCommand : IRequest<Driver>
    {
        public string Fullname { get; init; }
        public string LicenceNumber { get; init; }
        public int TruckId { get; set; }
        public IFormFile? ImageFile { get; set; }
        public AddressCreateCommand address { get; init; }
    }

    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, Driver>
    {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateDriverCommandHandler> _logger;
        private readonly IFileUploadService _fileUploadService;

        public CreateDriverCommandHandler(IIdentityService identityService, IAppDbContext context, ILogger<CreateDriverCommandHandler> logger, IFileUploadService fileUploadService)
        {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _fileUploadService = fileUploadService;
        }

        public async Task<Driver> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {

            Truck found_truck = await _context.Trucks.FindAsync(request.TruckId);

            if (found_truck == null)
            {
                throw new Exception("Truck not found");
            }

            var transaction = _context.database.BeginTransaction();
            int imageId;

            try
            {

                //image uploading
                var response = await _fileUploadService.uploadFile(request.ImageFile, FileType.Image);
                if (!response.result.Succeeded)
                {
                    throw new Exception(String.Join(" , ", response.result.Errors));
                }

                imageId = response.Id;


                //address insertion
                Address new_address = new Address();
                new_address.Email = request.address.Email;
                new_address.Phone = request.address.Phone;
                new_address.Region = request.address.Region;
                new_address.City = request.address.City;
                new_address.Subcity = request.address.Subcity;
                new_address.Country = request.address.Country;
                new_address.POBOX = request.address.POBOX;

                _context.Addresses.Add(new_address);
                await _context.SaveChangesAsync(cancellationToken);

                //dirver insertion
                Driver new_driver = new Driver();
                new_driver.Fullname = request.Fullname;
                new_driver.LicenceNumber = request.LicenceNumber;
                new_driver.TruckId = request.TruckId;
                new_driver.AddressId = new_address.Id;
                new_driver.ImageId = imageId;

                _context.Drivers.Add(new_driver);
                await _context.SaveChangesAsync(cancellationToken);
                
                //commit changes
                transaction.CommitAsync();

                return new_driver;

            }
            catch (Exception ex)
            {
                //rollback when error happend
                transaction.RollbackAsync();
                throw ex;
            }

        }
    }
}