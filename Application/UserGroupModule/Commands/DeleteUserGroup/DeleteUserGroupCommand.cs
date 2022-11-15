

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.UserGroupModule.Commands.DeleteUserGroup
{
    public record DeleteUserGroupCommand : IRequest<bool>
    {
        public int Id { get; init; } 
    }
    public class DeleteUserGroupCommandHandler : IRequestHandler<DeleteUserGroupCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeleteUserGroupCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteUserGroupCommand request, CancellationToken cancellationToken)
        {
            var oldGroup = await _context.UserGroups.FindAsync(request.Id);
            if (oldGroup == null)
            {
                throw new NotFoundException("UserGroup", new { Id = request.Id });
            }
          
            _context.UserGroups.Remove(oldGroup);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
