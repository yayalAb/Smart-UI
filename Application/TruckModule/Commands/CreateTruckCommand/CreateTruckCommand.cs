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

namespace Application.TruckModule.Commands.CreateTruckCommand
{
    public record CreateTruckCommand : IRequest<Truck> {
        public string TruckNumber { get; init; }
        public string Type { get; init; }
        public float Capacity { get; init; }
        public byte[]? Image { get; set; }
    }

    public class CreateTruckCommandHandler : IRequestHandler<CreateTruckCommand, Truck> {

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

        public async Task<Truck> Handle(CreateTruckCommand request, CancellationToken cancellationToken) {

            // //image uploading
            // var response = await _fileUploadService.GetFileByte(request.ImageFile, FileType.Image);
            // if (!response.result.Succeeded)
            // {
            //     throw new Exception(String.Join(" , ", response.result.Errors));
            // }

            Truck truck = new Truck();
            truck.TruckNumber = request.TruckNumber;
            truck.Type = request.Type;
            truck.Capacity = request.Capacity;
            truck.Image = request.Image;

            _context.Trucks.Add(truck);
            await _context.SaveChangesAsync(cancellationToken);

            return truck;

        }

    }
}