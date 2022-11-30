using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Application.GoodModule.Commands.CreateGoodCommand;
using AutoMapper;
using Application.Common.Models;

namespace Application.ContainerModule.Commands.CreateContainer
{
    public record CreateContainerCommand : IRequest<CustomResponse>
    {
        public string ContainerNumber { get; set; }
        public string SealNumber { get; set; }
        public string Location { get; set; }
        public float Size { get; set; }
        public int LocationPortId { get; set; }
        public int OperationId { get; set; }
        public ICollection<CreateGoodCommand> Goods { get; set; }
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
                    good.ContainerId = newContainer.Id;
                    _context.Goods.Add(_mapper.Map<Good>(good));
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
