namespace Kino.Models.Dtos.Authentication;

public class AuthenticationResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string RoleName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public AuthenticationResponse(User user, string token)
    {
        Id = user.Id;
        Email = user.Email;
        RoleName = user.RoleId.ToString();
        Token = token;
        Name = user.Name;
    }
}