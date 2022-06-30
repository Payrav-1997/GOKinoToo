using Kino.Enums;
using Kino.Models;
using Kino.Models.Dtos.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kino.Controllers;

public class AuthController : Controller
{
    private readonly DataContext _context;

    public AuthController(DataContext context)
    {
        _context = context;
    }
    [HttpGet]
    public  IActionResult Login()
    {
       return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(AuthenticationRequest loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => loginDto.Email.Contains(x.Email));
        if (user != null)
        {
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                ModelState.AddModelError("", "Пароли не совпадают!");
                return View(loginDto);
            }
            return Redirect("Films");
        }
        ModelState.AddModelError("","Пользователь не найден!");
         
        return View(loginDto);
        
    }

    [HttpGet]
    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registration(UserForRegistrationDto registrationDto)
    {
        var checkUserPhone = await CheckUserEmail(registrationDto.Email);
        if (checkUserPhone != null)
        {
            ModelState.AddModelError("","Пользователь существует!");
            return View(registrationDto);
        }
        var user = new User()
        {
            Id = new Guid(),
            Name = registrationDto.Name,
            Password =  BCrypt.Net.BCrypt.HashPassword(registrationDto.Password),
            RoleId = Role.User,
            CreateDate = DateTimeOffset.UtcNow,
            Email = registrationDto.Email
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return Redirect("Login");
    }

    private async Task<User> CheckUserEmail(string registrationDtoPhone)
    {
      return  await _context.Users.SingleOrDefaultAsync(x => registrationDtoPhone.Contains(x.Email));
    }

    public IActionResult Error()
    {
        return View();
    }
}