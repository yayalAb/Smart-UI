

using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserGroupModule.Commands.UpdateUserGroup
{
    public record UpdateUserGroupCommand : IRequest<CustomResponse>
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Responsiblity { get; init; }
        public List<FetchUserRoleDto> UserRoles { get; set; }
    }
    public class UpdateUserGroupCommandHandler : IRequestHandler<UpdateUserGroupCommand, CustomResponse>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public UpdateUserGroupCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CustomResponse> Handle(UpdateUserGroupCommand request, CancellationToken cancellationToken)
        {
            var executionStrategy = _context.database.CreateExecutionStrategy();
            return await executionStrategy.ExecuteAsync(
               async () =>
               {
                   using (var transaction = _context.database.BeginTransaction())
                   {
                       List<AppUserRole> userRoles = _mapper.Map<List<FetchUserRoleDto>, List<AppUserRole>>(request.UserRoles);

                       try
                       {
                           // checking if user group exists
                           var oldGroup = await _context.UserGroups.FindAsync(request.Id);
                           if (oldGroup == null)
                           {
                               throw new GhionException(CustomResponse.NotFound("UserGroup not found!"));
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
                           return CustomResponse.Succeeded("User Group updated");

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
