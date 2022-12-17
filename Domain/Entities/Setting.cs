
namespace Domain.Entities;

public class Setting
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Port { get; set; }
    public string Host { get; set; }
    public string Protocol { get; set; }
    public string Username { get; set; }
    public int CompanyId { get; set; }

    public Company DefaultCompany { get; set; } = null!;
}