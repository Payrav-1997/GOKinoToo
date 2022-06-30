namespace Kino.Models;

public class Genre
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public DateTimeOffset CreateDate { get; set; }
    public virtual ICollection<Film> Films { get; set; }
}