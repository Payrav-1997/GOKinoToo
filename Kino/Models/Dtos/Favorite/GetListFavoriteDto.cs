namespace Kino.Models.Dtos.Favorite;

public class GetListFavoriteDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string  ImagePath { get; set; }
    public Guid  GenreId { get; set; }
    public bool InTrend { get; set; }
}