using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Component.Commands.createComponent

{
    public record CreateComponentCommand : IRequest<CustomResponse>
    {
         public string ComponetName { get; init; }
        public string FormgroupName { get; init; }
        public int NoOfFeildsINRow { get; init; }
        public ICollection<feildsDto> Filds { get; init; }
    }
    public class CreateComponentCommandHandler : IRequestHandler<CreateComponentCommand, CustomResponse>
    {
         private readonly IAppDbContext _context;
         private readonly IMapper _mapper;

        public CreateComponentCommandHandler(IAppDbContext context, IMapper mapper)
          {
        _context = context;
        _mapper = mapper;
       }
        public async Task<CustomResponse> Handle(CreateComponentCommand request, CancellationToken cancellationToken)
        {
             
            // ProjectModel newProject = new ProjectModel
            // {
            //     ProjectName = request.ProjectName
            // };
            _context.Components.Add(_mapper.Map<ComponentModel>(request));
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("Project created!");

        }
    }
}
