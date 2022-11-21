using MediatR;
using Domain.Entities;
using Domain.Enums;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Application.AddressModule.Commands.AddressCreateCommand;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Application.Common.Models;
using Application.Common.Exceptions;

namespace Application.DriverModule.Commands.CreateDriverCommand
{
    public record CreateDriverCommand : IRequest<CustomResponse>
    {
        public string Fullname { get; init; }
        public string LicenceNumber { get; init; }
        public byte[]? Image { get; set; }
        public AddressCreateCommand address { get; init; }
    }

    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, CustomResponse>
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

        public async Task<CustomResponse> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {

            var executionStrategy = _context.database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(async () =>
            {

                using (var transaction = _context.database.BeginTransaction())
                {

                    // byte[]? image;

                    try

                    {

                        // //image uploading
                        // var response = await _fileUploadService.GetFileByte(request.ImageFile, FileType.Image);
                        // if (!response.result.Succeeded)
                        // {
                        //     throw new Exception(String.Join(" , ", response.result.Errors));
                        // }

                        // image = response.byteData;


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
                        new_driver.AddressId = new_address.Id;
                        new_driver.Image = request.Image;

                        _context.Drivers.Add(new_driver);
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync();

                        return CustomResponse.Succeeded("Driver Created Successfully");

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