using Kino.Enums;

namespace Kino.Models;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public Role RoleId { get; set; }
    public DateTimeOffset CreateDate { get; set; }
}