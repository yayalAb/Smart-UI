using System.Reflection.Metadata;
using MediatR;
using Domain.Entities;
using Domain.Enums;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Application.DriverModule.Commands.ChangeDriverImageCommand {

    public record ChangeDriverImage : IRequest<Driver> {
        public int Id {get; init;}
        public IFormFile ImageFile {get; init;}
    }

    public class ChangeDriverImageHandler : IRequestHandler<ChangeDriverImage, Driver> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<ChangeDriverImageHandler> _logger;
        private readonly IFileUploadService _fileUploadService;

        public ChangeDriverImageHandler (
            IIdentityService identityService, 
            IAppDbContext context, 
            ILogger<ChangeDriverImageHandler> logger, 
            IFileUploadService fileUploadService
        ) {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _fileUploadService = fileUploadService;
        }

        public async Task<Driver> Handle(ChangeDriverImage request, CancellationToken cancellationToken){
            Driver found_driver = await _context.Drivers.FindAsync(request.Id);

            if(found_driver == null){
                throw new Exception("Truck not found");
            }

            //image uploading
            var response = await _fileUploadService.GetFileByte(request.ImageFile, FileType.Image);
            if (!response.result.Succeeded)
            {
                throw new Exception(String.Join(" , ", response.result.Errors));
            }

            found_driver.Image = response.byteData;
            await _context.SaveChangesAsync(cancellationToken);

            return found_driver;

        }

    }

};