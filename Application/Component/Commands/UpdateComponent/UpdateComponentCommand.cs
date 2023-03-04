using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Component.Commands.createComponent;
using Domain.Entities;
using MediatR;

namespace Application.Component.Commands.UpdateComponent
{
    public class UpdateComponentCommand : IRequest<CustomResponse>
    {
        public int Id { get; set; }
       public string ComponetName { get; init; }
       public string ComponetNameVal { get; init; }
        public string FormgroupName { get; init; }
        public string FormgroupNameVal { get; init; }
        public int NoOfFeildsInRow { get; init; }
        public string NoOfFeildsInRowVal { get; init; }
        public ICollection<feildsDto> Filds { get; init; }
    }
    public class UpdateComponentCommandHandler : IRequestHandler<UpdateComponentCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public UpdateComponentCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(UpdateComponentCommand request, CancellationToken cancellationToken)
        {

            var SelectedComponet = await _context.Components.FindAsync(request.Id);
            if (SelectedComponet == null)
            {
                throw new GhionException(CustomResponse.NotFound("Componet not found!"));
            };
            SelectedComponet.ComponetName = request.ComponetName;
            SelectedComponet.FormgroupName = request.FormgroupName;
            SelectedComponet.NoOfFeildsInRow = request.NoOfFeildsInRow;
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Componet Updated Successfully!");

        }
    }
    }


