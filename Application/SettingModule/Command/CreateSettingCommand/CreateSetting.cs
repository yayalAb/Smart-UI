using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Application.Common.Models;

namespace Application.SettingModule.Command.CreateSettingCommand;

public record CreateSetting : IRequest<CustomResponse> {
    public string Email {get; set;}
    public string Password {get; set;}
    public int Port {get; set;}
    public string Host {get; set;}
    public string Protocol {get; set;}
    public string Username {get; set;}
}

public class CreateSettingHandler : IRequestHandler<CreateSetting, CustomResponse> {

    private readonly IAppDbContext _context;

    public CreateSettingHandler(IAppDbContext context) {
        _context = context;
    }

    public async Task<CustomResponse> Handle(CreateSetting request, CancellationToken cancellationToken){
        _context.Settings.Add(new Setting(){
            Email = request.Email,
            Password = request.Password,
            Port = request.Port,
            Host = request.Host,
            Protocol = request.Protocol,
            Username = request.Username
        });
        await _context.SaveChangesAsync(cancellationToken);
        return CustomResponse.Succeeded("Setting Saved Successfully");
    }
    
}