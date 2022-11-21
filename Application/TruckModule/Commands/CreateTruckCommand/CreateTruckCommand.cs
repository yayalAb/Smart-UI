using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Domain.Entities;
using Domain.Enums;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Application.Common.Exceptions;
using Application.Common.Models;

namespace Application.TruckModule.Commands.CreateTruckCommand
{
    public record CreateTruckCommand : IRequest<CustomResponse> {
        public string TruckNumber { get; init; }
        public string Type { get; init; }
        public float Capacity { get; init; }
        public IFormFile? ImageFile { get; set; }
    }

    public class CreateTruckCommandHandler : IRequestHandler<CreateTruckCommand, CustomResponse> {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateTruckCommandHandler> _logger;
        private readonly IFileUploadService _fileUploadService;

        public CreateTruckCommandHandler(
            IIdentityService identityService, 
            IAppDbContext context, 
            ILogger<CreateTruckCommandHandler> logger, 
            IFileUploadService fileUploadService
        ) {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _fileUploadService = fileUploadService;
        }

        public async Task<CustomResponse> Handle(CreateTruckCommand request, CancellationToken cancellationToken) {

            //image uploading
            var response = await _fileUploadService.GetFileByte(request.ImageFile, FileType.Image);
            if (!response.result.Succeeded)
            {
                throw new GhionException(CustomResponse.Failed(response.result.Errors));
            }

            Truck truck = new Truck();
            truck.TruckNumber = request.TruckNumber;
            truck.Type = request.Type;
            truck.Capacity = request.Capacity;
            truck.Image = response.byteData;

            _context.Trucks.Add(truck);
            await _context.SaveChangesAsync(cancellationToken);

            return CustomResponse.Succeeded("Truck Created!");

        }

    }
}