

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.Internal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserGroupModule.Commands.UpdateUserGroup
{
    public record UpdateUserGroupCommand : IRequest<int>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Responsiblity { get; init; }
        public List<UserRoleDto> UserRoles { get; set; }
    }
    public class UpdateUserGroupCommandHandler : IRequestHandler<UpdateUserGroupCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public UpdateUserGroupCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateUserGroupCommand request, CancellationToken cancellationToken)
        {
            var executionStrategy = _context.database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(
               async () =>
               {
                   using (var transaction = _context.database.BeginTransaction())
                   {
                       List<AppUserRole> userRoles = _mapper.Map<List<UserRoleDto>, List<AppUserRole>>(request.UserRoles);

                       try
                       {
                         // checking if user group exists
                           var oldGroup = await _context.UserGroups.FindAsync(request.Id);
                           if (oldGroup == null)
                           {
                               throw new NotFoundException("UserGroup", new { Id = request.Id });
                           }
                           //updating user group
                           oldGroup.Name = request.Name;
                           oldGroup.Responsiblity = request.Responsiblity;

                           _context.UserGroups.Update(oldGroup);
                           await _context.SaveChangesAsync(cancellationToken);
                           if (userRoles.ToArray().Length != Enum.GetNames(typeof(Page)).Length)
                           {
                               userRoles = AppUserRole.fillUndefinedRoles(userRoles);
                           }
                          //updating user roles 
                           _context.AppUserRoles.UpdateRange(userRoles);
                           await _context.SaveChangesAsync(cancellationToken);
                           await transaction.CommitAsync();
                           return oldGroup.Id;

                       }
                       catch (Exception)
                       {
                           await transaction.RollbackAsync();

                           throw;
                       }
                   }
               });

        }
    }
}
