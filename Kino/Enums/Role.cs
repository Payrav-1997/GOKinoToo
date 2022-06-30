using System.ComponentModel;

namespace Kino.Enums;

public enum Role
{
    [Description("Пользователь")]
    User = 1,
    [Description("Администратор")]
    Admin 
}