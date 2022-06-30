namespace Kino.Models.Dtos.Film;

public class GetFilmDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string  ImagePath { get; set; }
    public string  GenreName { get; set; }
    public bool InTrend { get; set; }
    public string Duration { get; set; }
    public string ProductionCompany { get; set; }
    public string Trailer  { get; set; }
    public string CountryOfOrigin  { get; set; }
    public string Director  { get; set; }
    public string SubtitleLanguage  { get; set; }
    public double IMDBRating  { get; set; }
    public string MPAARating  { get; set; }
    public DateTimeOffset  CreateDate { get; set; }
}