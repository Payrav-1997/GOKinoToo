using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Kino.Errors;
using Kino.Models;
using Kino.Models.Dtos.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace Kino.Controllers
{
    public class TicketsController : BaseController
    {
        public TicketsController(DataContext context) : base(context)
        {
        }

        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        } 
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTicketDto ticketDto)
        {
            var currentUserId = GetCurrentUserId();
            if (ticketDto == null)
                throw new ExceptionWithStatusCode(HttpStatusCode.NoContent, "Empty Ticket");
            var request = new Ticket
            {
                Id = new Guid(),
                FilmId = ticketDto.FilmId,
                UserId = currentUserId,
                DateIssued = DateTime.UtcNow,
                UnderName = ticketDto.UnderName,
                Token = ticketDto.Token
            };
            await Context.Tickets.AddAsync(request);
            await Context.SaveChangesAsync();

            return Ok();
        }
    }
}