namespace Kino.Models;

public class Ticket
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public Guid FilmId { get; set; }
    public Guid SeatId { get; set; }
    public Guid UserId { get; set; }
    public DateTime DateIssued { get; set; }
    public string UnderName { get; set; }
    public string Token { get; set; }
    public virtual Film Film { get; set; }
}