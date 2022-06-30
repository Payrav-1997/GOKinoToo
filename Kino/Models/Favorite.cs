namespace Kino.Models;

public class Favorite
{
    public Guid Id { get; set; }
    public Guid FilmId { get; set; }
    public Guid UserId { get; set; }
    public DateTimeOffset CreateDate { get; set; }
    public virtual Film Films { get; set; }
    public virtual User User { get; set; }
    
}