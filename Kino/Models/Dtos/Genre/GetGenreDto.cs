namespace Kino.Models.Dtos.Genre;

public class GetGenreDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public DateTimeOffset CreateDate { get; set; }
}