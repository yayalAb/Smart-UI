using Application.Button.Commands.CreateButtonCommand;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Button.Commands.UpdateButtonCommand
{
    public class UpdateButtonCommand : IRequest<CustomResponse>
    {
        public int Id { get; set; }
        public string ComponetName { get; init; }
        public ICollection<ButtonFeildsDto> buttonData { get; init; }
    }
    public class UpdateButtonCommandHandler : IRequestHandler<UpdateButtonCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public UpdateButtonCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(UpdateButtonCommand request, CancellationToken cancellationToken)
        {

            var SelectedButton = await _context.buttons.FindAsync(request.Id);
            if (SelectedButton == null)
            {
                throw new GhionException(CustomResponse.NotFound("Button not found!"));
            };
            SelectedButton.ComponetName = request.ComponetName;
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Button Updated Successfully!");

        }
    }
    }


