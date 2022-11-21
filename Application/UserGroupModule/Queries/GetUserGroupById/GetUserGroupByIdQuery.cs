
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserGroupModule.Queries.GetUserGroupById
{
    public record GetUserGroupByIdQuery : IRequest<UserGroupDto>
    {
        public int Id { get; init; }
    }
    public class GetUserGroupByIdQueryHandler : IRequestHandler<GetUserGroupByIdQuery, UserGroupDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetUserGroupByIdQueryHandler(IAppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async  Task<UserGroupDto> Handle(GetUserGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var oldGroup = await _context.UserGroups
                .Include(ug => ug.UserRoles)
                .ProjectTo<UserGroupDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ug => ug.Id == request.Id);
            if (oldGroup == null)
            {
                throw new GhionException(CustomResponse.NotFound("UserGroup"));
            }
            return oldGroup;    
        }
    }
}
