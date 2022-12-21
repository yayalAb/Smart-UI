
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.GoodModule.Commands.DeleteGood
{

    public record DeleteGoodCommand : IRequest<CustomResponse>
    {
        public int Id { get; set; }
      

    }



    public class DeleteGoodCommandHandler : IRequestHandler<DeleteGoodCommand, CustomResponse>
    {

        private readonly IIdentityService _identityService;
        private readonly IAppDbContext _context;
        private readonly ILogger<DeleteGoodCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteGoodCommandHandler(
            IIdentityService identityService,
            IAppDbContext context,
            ILogger<DeleteGoodCommandHandler> logger,
            IMapper mapper
        )
        {
            _identityService = identityService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CustomResponse> Handle(DeleteGoodCommand request, CancellationToken cancellationToken)
        {
            var good = await _context.Goods.FindAsync(request.Id);
            if(good == null){
                throw new GhionException(CustomResponse.NotFound($"good with id = {request.Id} is not found"));

            }
            _context.Goods.Remove(good);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("good deleted successfully!");
        }

    }

}