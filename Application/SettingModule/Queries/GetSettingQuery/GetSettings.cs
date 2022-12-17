using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.SettingModule.Queries.GetSettingQuery;

public record GetSettings : IRequest<Setting> { }

public class GetSettingsHandler : IRequestHandler<GetSettings, Setting>
{

    private readonly IAppDbContext _context;

    public GetSettingsHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Setting> Handle(GetSettings request, CancellationToken cancellationToken)
    {

        var setting = _context.Settings.FirstOrDefault();

        if (setting == null)
        {
            throw new GhionException(CustomResponse.Failed("Setting not found!"));
        }

        return setting;
    }

}