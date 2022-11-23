
using MediatR;
using Domain.Entities;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Application.GoodModule.Commands.CreateGoodCommand {

    public record CreateGoodCommand : IRequest<Good> {    
    public string? Description { get; set; }
    public string? HSCode { get; set; }
    public string? Manufacturer { get; set; }
    public float? Weight { get; set; }
    public float? Quantity { get; set; }
    public int? NumberOfPackages {get; set;}
    public string Type {get; set;} 
    public string ChasisNumber {get; set; }
    public string EngineNumber {get; set;}
    public string ModelCode { get; set; }
    public int? ContainerId { get; set; }
    }

    public class CreateGoodCommandHandler : IRequestHandler<CreateGoodCommand, Good>
    {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateGoodCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateGoodCommandHandler(
            IIdentityService identityService,
            IAppDbContext context,
            ILogger<CreateGoodCommandHandler> logger,
            IMapper mapper
        ) {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Good> Handle(CreateGoodCommand request, CancellationToken cancellationToken)
        {

            Container found_container = await _context.Containers.FindAsync(request.ContainerId);

            if (found_container == null) {
                throw new Exception("Container not found");
            }

            Good new_good = _mapper.Map<Good>(request);
            _context.Goods.Add(new_good);
            await _context.SaveChangesAsync(cancellationToken);

            return new_good;

        }

    }

};