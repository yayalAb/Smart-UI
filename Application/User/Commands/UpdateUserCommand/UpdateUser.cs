using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.User.Commands.AuthenticateUser;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.AddressModule.Commands.AddressUpdateCommand;
using Microsoft.EntityFrameworkCore;

namespace Application.User.Commands.UpdateUserCommand;

public record UpdateUser : IRequest<string>
{
    public string Id { get; init; }
    public string FullName { get; init; }
    public string UserName { get; init; }
    public byte State { get; init; }
    public int UserGroupId { get; init; }
    public AddressUpdateCommand Address { get; set; }

}

public class UpdateUserHandler : IRequestHandler<UpdateUser, string>
{
    private readonly IIdentityService _identityService;
    private readonly IAppDbContext _context;
    private readonly ILogger<UpdateUserHandler> _logger;
    private readonly IMapper _mapper;

    public UpdateUserHandler(
        IIdentityService identityService,
        IAppDbContext context,
        ILogger<UpdateUserHandler> logger,
        IMapper mapper
    )
    {
        _identityService = identityService;
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<string> Handle(UpdateUser request, CancellationToken cancellationToken)
    {

        // _context.database.BeginTransaction();
        var executionStrategy = _context.database.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(
            async () =>
           {

               using (var transaction = _context.database.BeginTransaction())
               {

                   try
                   {

                       var response = await _identityService.UpdateUser(request.Id, request.FullName, request.UserName, request.Address.Email, request.State, request.UserGroupId);

                       if (!response.Succeeded)
                       {
                           throw new CannotUpdateUserException(response.Errors.ToList());
                       }

                       Address found_address = await _context.Addresses.FindAsync(request.Address.Id);

                       if (found_address == null)
                       {
                           throw new Exception("address not found!");
                       }

                       found_address.Email = request.Address.Email;
                       found_address.Phone = request.Address.Phone;
                       found_address.Region = request.Address.Region;
                       found_address.City = request.Address.City;
                       found_address.Subcity = request.Address.Subcity;
                       found_address.Country = request.Address.Country;
                       found_address.POBOX = request.Address.POBOX ?? "";

                       await _context.SaveChangesAsync(cancellationToken);
                       await transaction.CommitAsync();

                       return "User updated successfully";

                   }
                   catch (Exception ex)
                   {
                       await transaction.RollbackAsync();
                       throw ex;
                   }

               }
           }
        );

    }

}