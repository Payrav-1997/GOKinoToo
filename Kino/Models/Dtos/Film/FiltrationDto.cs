namespace Kino.Models.Dtos.Film;

public class FiltrationDto : RequestParameters
{
    public string Genre { get; set; }
    public string Search { get; set; }
    public bool IsTrending { get; set; }
}

