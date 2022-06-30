namespace Kino.Models.Dtos.Film;

public class CreateFilmDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid GenreId { get; set; }
    public IFormFile[] Image { get; set; }
    public bool InTrend { get; set; }
    public string Duration { get; set; } 
    public string ProductionCompany { get; set; }
    public string Trailer  { get; set; }
    public string CountryOfOrigin  { get; set; }
    public string Director  { get; set; }
    public string SubtitleLanguage  { get; set; }
    public double IMDBRating  { get; set; }
    public string MPAARating  { get; set; }

    public List<Models.Genre> Genres { get; set; }
}