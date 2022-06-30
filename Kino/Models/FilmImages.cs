namespace Kino.Models;

public class FilmImages
{
    public Guid Id { get; set; }
    public Guid FilmId { get; set; }
    public string ImagePath { get; set; }
    public string Title { get; set; }
    public virtual Film Film { get; set; }
}