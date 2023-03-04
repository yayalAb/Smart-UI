using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Button.Commands.CreateButtonCommand

{
    public record CreateButtonCommand : IRequest<CustomResponse>
    {
         public string ComponetName { get; init; }
        public ICollection<ButtonFeildsDto> buttonData { get; init; }
    }
    public class CreateButtonCommandHandler : IRequestHandler<CreateButtonCommand, CustomResponse>
    {
         private readonly IAppDbContext _context;
         private readonly IMapper _mapper;

        public CreateButtonCommandHandler(IAppDbContext context, IMapper mapper)
          {
        _context = context;
        _mapper = mapper;
       }
        public async Task<CustomResponse> Handle(CreateButtonCommand request, CancellationToken cancellationToken)
        {
             
            // ProjectModel newProject = new ProjectModel
            // {
            //     ProjectName = request.ProjectName
            // };
            _context.buttons.Add(_mapper.Map<ButtonModel>(request));
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Button created!");

        }
    }
}
