namespace Kino.Models.Dtos.Ticket;

public class CreateTicketDto
{
    public Guid FilmId { get; set; }
    public string UnderName { get; set; }
    public string Token { get; set; }
}