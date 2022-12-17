using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.TruckModule.Commands.CreateTruckCommand
{
    public record CreateTruckCommand : IRequest<CustomResponse>
    {
        public string TruckNumber { get; init; }
        public string Type { get; init; }
        public float Capacity { get; init; }
        public string PlateNumber { get; set; }
        public string? Image { get; set; }
    }

    public class CreateTruckCommandHandler : IRequestHandler<CreateTruckCommand, CustomResponse>
    {

        private readonly IMapper _mapper;
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateTruckCommandHandler> _logger;
        private readonly IFileUploadService _fileUploadService;

        public CreateTruckCommandHandler(
            IMapper mapper,
            IAppDbContext context,
            ILogger<CreateTruckCommandHandler> logger,
            IFileUploadService fileUploadService
        )
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
            _fileUploadService = fileUploadService;
        }

        public async Task<CustomResponse> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
        {

            // //image uploading
            // var response = await _fileUploadService.GetFileByte(request.ImageFile, FileType.Image);
            // if (!response.result.Succeeded)
            // {
            //     throw new Exception(String.Join(" , ", response.result.Errors));
            // }

            Truck truck = _mapper.Map<Truck>(request);

            _context.Trucks.Add(truck);
            await _context.SaveChangesAsync(cancellationToken);

            return CustomResponse.Succeeded("Truck Created!");

        }

    }
}