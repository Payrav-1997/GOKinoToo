using System.Net;
using Kino.Common;
using Kino.Common.IService;
using Kino.Errors;
using Kino.Models;
using Kino.Models.Dtos.Film;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kino.Controllers;

public class FilmsController : Controller
{
    private readonly IFileService _fileService;
    private readonly DataContext _context;

    public FilmsController(DataContext context,IFileService fileService)
    {
        _fileService = fileService;
        _context = context;
    }
   
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var genre = new CreateFilmDto()
        {
            Genres = await _context.Genres.ToListAsync()
        };
        return View(genre);
    }
   
    
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateFilmDto filmDto)
    {
        var request = new Film()
        {
            Name = filmDto.Name,
            InTrend = filmDto.InTrend,
            ImagePath = await _fileService.AddFileAsync(nameof(Film), filmDto.Image),
            CreateDate = DateTimeOffset.UtcNow,
            GenreId =filmDto.GenreId,
            Description = filmDto.Description,
            Duration = filmDto.Duration,
            ProductionCompany = filmDto.ProductionCompany,
            Trailer = filmDto.Trailer,
            SubtitleLanguage = filmDto.SubtitleLanguage,
            CountryOfOrigin = filmDto.CountryOfOrigin,
            IMDBRating = filmDto.IMDBRating,
            Director = filmDto.Director,
            RARSRating = filmDto.MPAARating
        };
        await _context.AddAsync(request);
        await _context.SaveChangesAsync();
        return Redirect("Films");
    }

    [HttpGet]
    public  async Task<IActionResult> Update(int id)
    {
       var film = await _context.Films.FirstOrDefaultAsync(x => id.Equals(x.Id));
        return View(film);
    }
    [HttpPost]
    public async Task<IActionResult> Update([FromQuery] UpdateFilmDto filmDto)
    {
        var film = await _context.Films.FirstOrDefaultAsync(x => filmDto.Equals(x.Id));
        if (film != null)
        {
            film.Description = filmDto.Description;
            film.Genre.Id = filmDto.GenreId;
            film.Name = filmDto.Name;
            // if (filmDto.Image != null)
            //     film.ImagePath = await _fileService.AddFileAsync(nameof(Film), filmDto.Image);
            film.InTrend = filmDto.InTrend;
            film.Duration = filmDto.Duration;
            film.ProductionCompany = filmDto.ProductionCompany;
            film.Trailer = filmDto.Trailer;
            film.SubtitleLanguage = filmDto.SubtitleLanguage;
            film.CountryOfOrigin = filmDto.CountryOfOrigin;
            film.IMDBRating = filmDto.IMDBRating;
            film.Director = filmDto.Director;
            film.RARSRating = filmDto.MPAARating;
            _context.Films.Update(film);
        }
        await _context.SaveChangesAsync();
        return Redirect("Films");
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var films = await _context.Films.Select(x=>new GetListFilmDto()
        {
            Id = x.Id,
            Director = x.Director,
            Name = x.Name,
            Duration = x.Duration,
            Trailer = x.Trailer,
            CreateDate = x.CreateDate,
            GenreName = x.Genre.Name,

           
        }).ToListAsync();
        return View(films);

    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid id)
    {
        var film = await _context.Films.FindAsync(id);
        if (film == null) return View();
        var response = new GetListFilmDto()
        {
            ImagePath = film.ImagePath,
            InTrend = film.InTrend,
            Description = film.Description,
            ProductionCompany = film.ProductionCompany,
            SubtitleLanguage = film.SubtitleLanguage,
            CountryOfOrigin = film.CountryOfOrigin,
            IMDBRating = film.IMDBRating,
            MPAARating = film.RARSRating
        };
        return View(response);

    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var genre = await _context.Films.FindAsync(id);
        if(genre == null)
            throw new ExceptionWithStatusCode(HttpStatusCode.NotFound, "Film not found!");
        if (genre.ImagePath != null)
        {
            
            var rootDirectory = Path.GetFullPath("wwwroot/images");
            var substring = genre.ImagePath.Substring(8);
            var path = rootDirectory + substring;
            System.IO.File.Delete($"{path}");
        }

        _context.Films.Remove(genre);
        await _context.SaveChangesAsync();
        var response = new Response()
        {
            statusCode = (int) HttpStatusCode.OK,
            message = "Deleted"
        };
        return RedirectToAction("GetAll");
    }


}