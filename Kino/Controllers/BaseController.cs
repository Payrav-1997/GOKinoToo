using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kino.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kino.Controllers
{
    public class BaseController : Controller
    {
        protected readonly DataContext Context;

        public BaseController(DataContext context)
        {
            Context = context;
        }

        protected Guid GetCurrentUserId()=>
            HttpContext.User.Identity is not ClaimsIdentity identity ? Guid.Empty : Guid.Parse(HttpContext.User.Claims
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .Select(x => x.Value)
                .FirstOrDefault() ?? string.Empty);
    }
}