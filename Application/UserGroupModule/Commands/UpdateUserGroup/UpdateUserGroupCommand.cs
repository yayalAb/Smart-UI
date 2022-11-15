

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using FluentValidation.Internal;
using MediatR;

namespace Application.UserGroupModule.Commands.UpdateUserGroup
{
    public record UpdateUserGroupCommand : IRequest<int>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Responsiblity { get; init; }
    }
    public class UpdateUserGroupCommandHandler : IRequestHandler<UpdateUserGroupCommand, int>
    {
        private readonly IAppDbContext _context;

        public UpdateUserGroupCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateUserGroupCommand request, CancellationToken cancellationToken)
        {
            var oldGroup = await _context.UserGroups.FindAsync(request.Id); 
            if(oldGroup == null)
            {
                throw new NotFoundException("UserGroup", new { Id = request.Id });
            }
            oldGroup.Name = request.Name;
            oldGroup.Responsiblity = request.Responsiblity;

            _context.UserGroups.Update(oldGroup);
            await _context.SaveChangesAsync(cancellationToken);
            return oldGroup.Id;
        }
    }
}
