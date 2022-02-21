using Microsoft.AspNetCore.Mvc;
using PracticaCore2ART.Filters;
using PracticaCore2ART.Models;
using PracticaCore2ART.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PracticaCore2ART.Controllers
{
    public class UsersController : Controller
    {
        private LibrosRepository repo;


        public UsersController(LibrosRepository repo)
        {
            this.repo = repo;
        }


        [AuthorizeUsers]
        public IActionResult Perfil()
        {
            int id = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            User user = this.repo.FindUser(id);
            return View(user);
        }
    }
}
