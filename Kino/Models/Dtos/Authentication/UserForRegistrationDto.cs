namespace Kino.Models.Dtos.Authentication;

public class UserForRegistrationDto
{
    public string Email { get; set; }
    
    public string Name { get; set; }
    public string Password { get; set; }
}