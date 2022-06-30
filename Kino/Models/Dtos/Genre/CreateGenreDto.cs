namespace Kino.Models.Dtos.Genre;

public class CreateGenreDto
{
    public string Name { get; set; }
    public IFormFile[] ImagePath { get; set; }
}