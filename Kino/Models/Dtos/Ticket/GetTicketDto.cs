using Kino.Models.Dtos.Film;

namespace Kino.Models.Dtos.Ticket;

public class GetTicketDto
{
    public Guid Id{ get; set; }
    public GetFilmDto GetFilmDto { get; set; }
    public string  UnderName { get; set; }
    public string Token  { get; set; }
}
