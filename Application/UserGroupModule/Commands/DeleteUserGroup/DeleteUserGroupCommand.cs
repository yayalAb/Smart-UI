

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.UserGroupModule.Commands.DeleteUserGroup
{
    public record DeleteUserGroupCommand : IRequest<CustomResponse>
    {
        public int Id { get; init; }
    }
    public class DeleteUserGroupCommandHandler : IRequestHandler<DeleteUserGroupCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;

        public DeleteUserGroupCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomResponse> Handle(DeleteUserGroupCommand request, CancellationToken cancellationToken)
        {
            var oldGroup = await _context.UserGroups.FindAsync(request.Id);
            if (oldGroup == null)
            {
                throw new GhionException(CustomResponse.NotFound($"UserGroup with id = {request.Id} is not found"));
            }

            _context.UserGroups.Remove(oldGroup);
            await _context.SaveChangesAsync(cancellationToken);
            return CustomResponse.Succeeded("UserGroup deleted successfully!");
        }
    }
}
