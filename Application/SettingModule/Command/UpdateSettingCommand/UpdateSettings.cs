using System.Net;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Models;

namespace Application.SettingModule.Command.UpdateSettingCommand;

public record UpdateSetting : IRequest<CustomResponse> {
    public int Id {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
    public string Port {get; set;}
    public string Host {get; set;}
    public string Protocol {get; set;}
    public string Username {get; set;}
}

public class UpdateSettingHandler : IRequestHandler<UpdateSetting, CustomResponse> {

    private readonly IAppDbContext _context;

    public UpdateSettingHandler(IAppDbContext context) {
        _context = context;
    }

    public async Task<CustomResponse> Handle(UpdateSetting request, CancellationToken cancellationToken){
        
        var setting = await _context.Settings.FindAsync(request.Id);
        
        if(setting == null){
            throw new GhionException(CustomResponse.Failed("setting not found!"));
        }

        setting.Email = request.Email;
        setting.Password = request.Password;
        setting.Port = request.Port;
        setting.Host = request.Host;
        setting.Protocol = request.Protocol;
        setting.Username = request.Username;

        await _context.SaveChangesAsync(cancellationToken);
        
        return CustomResponse.Succeeded("Setting is Saved Successfully!");

    } 

}