using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.SettingModule.Command.UpdateSettingCommand;

public record UpdateSetting : IRequest<string> {
    public int Id {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
    public string Port {get; set;}
    public string Host {get; set;}
    public string Protocol {get; set;}
    public string Username {get; set;}
}

public class UpdateSettingHandler : IRequestHandler<UpdateSetting, string> {

    private readonly IAppDbContext _context;

    public UpdateSettingHandler(IAppDbContext context) {
        _context = context;
    }

    public async Task<string> Handle(UpdateSetting request, CancellationToken cancellationToken){
        
        var setting = await _context.Settings.FindAsync(request.Id);
        
        if(setting == null){
            throw new Exception("setting not found!");
        }

        setting.Email = request.Email;
        setting.Password = request.Password;
        setting.Port = request.Port;
        setting.Host = request.Host;
        setting.Protocol = request.Protocol;
        setting.Username = request.Username;

        await _context.SaveChangesAsync(cancellationToken);
        
        return "Setting is Saved Successfully!";

    } 

}