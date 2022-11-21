

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using FluentValidation.Internal;
using MediatR;

namespace Application.UserGroupModule.Commands.UpdateUserGroup
{
    public record UpdateUserGroupCommand : IRequest<CustomResponse>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Responsiblity { get; init; }
    }
    public class UpdateUserGroupCommandHandler : IRequestHandler<UpdateUserGroupCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public UpdateUserGroupCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(UpdateUserGroupCommand request, CancellationToken cancellationToken)
        {
            var oldGroup = await _context.UserGroups.FindAsync(request.Id); 
            if(oldGroup == null)
            {
                throw new GhionException(CustomResponse.NotFound("UserGroup not found!"));
            }
            oldGroup.Name = request.Name;
            oldGroup.Responsiblity = request.Responsiblity;

            _context.UserGroups.Update(oldGroup);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("User Group updated");
        }
    }
}
