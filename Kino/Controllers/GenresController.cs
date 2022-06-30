using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Kino.Common;
using Kino.Common.IService;
using Kino.Errors;
using Kino.Models;
using Kino.Models.Dtos.Genre;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kino.Controllers
{
    public class GenresController : BaseController
    {
        private readonly IFileService _fileService;
        
        public GenresController(IFileService fileService, DataContext context) : base(context)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var genre = await Context.Genres.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (genre == null)
                throw new ExceptionWithStatusCode(HttpStatusCode.NotFound, "Genre not found");
            var result = new GetGenreDto()
            {
                Id = genre.Id,
                ImagePath = genre.ImagePath,
                Name = genre.Name,
                CreateDate = genre.CreateDate.DateTime
            };
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreDto genreDto)
        {
            var genre = new Genre()
            {
                Name = genreDto.Name,
                ImagePath = await _fileService.AddFileAsync(nameof(Genre), genreDto.ImagePath),
                CreateDate = DateTimeOffset.UtcNow
            };
            await Context.Genres.AddAsync(genre);
            await Context.SaveChangesAsync();
            
            return Redirect("GetAll");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await Context.Genres.ToListAsync();
            var res = genres.Select(x => new GetListGenreDto()
            {
                Id = x.Id,
                Name = x.Name,
                ImagePath = x.ImagePath,
                CreateDate = x.CreateDate.DateTime
            }).ToList();
            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var genre = await Context.Genres.FindAsync(id);
            if(genre == null)
                throw new ExceptionWithStatusCode(HttpStatusCode.NotFound, "Genre not found!");
            if (genre.ImagePath != null)
            {
            
                var rootDirectory = Path.GetFullPath("wwwroot/images");
                var substring = genre.ImagePath.Substring(8);
                var path = rootDirectory + substring;
                System.IO.File.Delete($"{path}");
            }

            Context.Genres.Remove(genre);
            await Context.SaveChangesAsync();
            var response = new Response()
            {
                statusCode = (int) HttpStatusCode.OK,
                message = "Deleted"
            };
            return RedirectToAction("GetAll");
        }
    }
}