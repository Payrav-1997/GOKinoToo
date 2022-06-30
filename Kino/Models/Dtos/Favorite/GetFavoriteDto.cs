namespace Kino.Models.Dtos.Favorite;

public class GetFavoriteDto
{
    public Guid FilmId { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    public string  ImagePath { get; set; }
    public bool InTrend { get; set; }
}