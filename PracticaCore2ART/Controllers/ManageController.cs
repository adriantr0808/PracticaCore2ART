using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PracticaCore2ART.Models;
using PracticaCore2ART.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PracticaCore2ART.Controllers
{
    public class ManageController : Controller
    {

        private LibrosRepository repo;

        public ManageController(LibrosRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Login()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            User user = this.repo.ExisteUser(email, password);
            if (user != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity
                    (CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                Claim claimName = new Claim(ClaimTypes.Name, user.Nombre);
                Claim claimRole = new Claim(ClaimTypes.Role, user.Apellidos);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, user.Id_Usuario.ToString());
                Claim claimImagen = new Claim("IMAGEN", user.Foto);

                identity.AddClaim(claimName);
                identity.AddClaim(claimRole);
                identity.AddClaim(claimId);
                identity.AddClaim(claimImagen);

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();
                string id = TempData["id"].ToString();

                return RedirectToAction(action, controller, new { id = id });
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
            }

            return View();
            
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync
               (CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ErrorAcceso()
        {
            return View();
        }

    }
}
