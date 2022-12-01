
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using AutoMapper;
using Application.Common.Models;
using Application.GoodModule;

namespace Application.ContainerModule.Commands.CreateContainer
{
    public record CreateContainerCommand : IRequest<CustomResponse>
    {
        public string ContainerNumber { get; set; } = null!;
        public string SealNumber { get; set; } = null!;
        public string Location { get; set; } = null!;
        public float Size { get; set; }
        public int LocationPortId { get; set; }
        public int OperationId { get; set; }
        public ICollection<GoodDto> Goods { get; set; }= null!;
    }

    public class CreateContainerCommandHandler : IRequestHandler<CreateContainerCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;
        private readonly IFileUploadService _fileUploadService;
        private readonly IMapper _mapper;

        public CreateContainerCommandHandler(IAppDbContext context, IFileUploadService fileUploadService, IMapper mapper)
        {
            _context = context;
            _fileUploadService = fileUploadService;
            _mapper = mapper;
        }
        public async Task<CustomResponse> Handle(CreateContainerCommand request, CancellationToken cancellationToken)
        {
            using var transaction = _context.database.BeginTransaction();

            try
            {

                Container newContainer = _mapper.Map<Container>(request);
                _context.Containers.Add(newContainer);
                await _context.SaveChangesAsync(cancellationToken);

                //inserting goods

                foreach (var good in request.Goods)
                {
                    var mappedGood = _mapper.Map<Good>(good);
                    mappedGood.ContainerId = newContainer.Id;
                    _context.Goods.Add(mappedGood);
                }

                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync();
                return CustomResponse.Succeeded("Container Created");

            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }


        }
    }
}
