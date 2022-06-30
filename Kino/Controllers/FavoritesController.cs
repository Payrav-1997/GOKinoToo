using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Kino.Errors;
using Kino.Extensions;
using Kino.Models;
using Kino.Models.Dtos;
using Kino.Models.Dtos.Favorite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kino.Controllers
{
    public class FavoritesController : BaseController
    {
        public FavoritesController(DataContext context) : base(context)
        {
        }
        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        } 
        [HttpPost]
        public async Task<IActionResult> Create(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var favorite = Context.Favorites.FirstOrDefaultAsync(x => x.FilmId.Equals(id));
            if (favorite == null) throw new ExceptionWithStatusCode(HttpStatusCode.NotFound, "Favorite not found");
            var response = new Favorite()
            {
                FilmId = id,
                CreateDate = DateTimeOffset.UtcNow,
                UserId = currentUserId
            };
            await Context.Favorites.AddAsync(response);
            await Context.SaveChangesAsync();
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(FavoriteFiltrationDto filtrationDto)
        {
            var favorite = Context.Favorites.AsQueryable();
            if (filtrationDto.Search != null)
                favorite = favorite.Where(x => x.Films.Name.ToLower().Contains(filtrationDto.Search.ToLower().Trim())||
                                               x.Films.Name.ToLower().Contains(filtrationDto.Search.ToLower().Trim()));
            var count = favorite.CountAsync();
            var favorites = await favorite.OrderByDescending(x=>x.Id).Paginate(filtrationDto).Select(fl => new GetListFavoriteDto()
            {
                Description = fl.Films.Description,
                Name = fl.Films.Name,
                ImagePath = fl.Films.ImagePath,
                InTrend = fl.Films.InTrend,
                GenreId = fl.Films.GenreId
            }).ToListAsync();
            var result = new PagedResponse<List<GetListFavoriteDto>>(favorites, await count, filtrationDto);

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var film = await Context.Favorites.FindAsync(id);
            if (film == null)
                throw new ExceptionWithStatusCode(HttpStatusCode.NotFound, "Notfound");
            Context.Favorites.Remove(film);
            await Context.SaveChangesAsync();
            return RedirectToAction("GetAll");
        }
    }
}   