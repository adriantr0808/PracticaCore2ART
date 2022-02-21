using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracticaCore2ART.Extensions;
using PracticaCore2ART.Filters;
using PracticaCore2ART.Models;
using PracticaCore2ART.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PracticaCore2ART.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private LibrosRepository repo;
        public HomeController(ILogger<HomeController> logger, LibrosRepository repo )
        {
            _logger = logger;
            this.repo = repo;
        }

        public IActionResult Index(int? idlibro, int?posicion)
        {
            if (posicion == null)
            {
                posicion = 0;
            }

            int numerolibros = 0;


            List<Libro> libros = this.repo.GetLibros(posicion.Value, ref numerolibros);

      


            int siguiente = posicion.Value + 5;
            if (siguiente >= numerolibros)
            {
                siguiente = 0;
            }
            int anterior = posicion.Value - 5;
            if (anterior < 0)
            {
                anterior = numerolibros - 5;
            }
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;


            if (idlibro != null)
            {
                List<int> idslibro;
                if (HttpContext.Session.GetString("IDSLIBROS") == null)
                {
                    idslibro = new List<int>();
                }
                else
                {
                    idslibro = HttpContext.Session.GetObject<List<int>>("IDSLIBROS");
                }

                idslibro.Add(idlibro.Value);

                HttpContext.Session.SetObject("IDSLIBROS", idslibro);

                ViewData["LIBROS"] = "Libros: " + idslibro.Count;
            }
            return View(libros);
        }
      
        public IActionResult CarritoLibros(int? ideliminar)
        {
            List<int> idslibro = HttpContext.Session.GetObject<List<int>>("IDSLIBROS");
          
            if(idslibro == null)
            {
                ViewData["MENSAJE"] = "No has añadido ningun libro al carrito";
                return View();

            }
            else
            {
                if (ideliminar != null)
                {
                    idslibro.Remove(ideliminar.Value);
                    if (idslibro.Count == 0)
                    {

                        HttpContext.Session.Remove("IDSLIBROS");
                    }
                    else
                    {
                        HttpContext.Session.SetObject("IDSLIBROS", idslibro);
                    }

                }

                List<Libro> libros = this.repo.GetLibrosSession(idslibro);
                return View(libros);
            }
        }

        [AuthorizeUsers]
        [HttpPost]
        public IActionResult CarritoLibros(List<int> idlibro)
        {
            int id = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            this.repo.InsertPedido(idlibro, id);

            HttpContext.Session.Remove("IDSLIBROS");

            return RedirectToAction("VistaPedido", "VistaPedidos");
        }

        public IActionResult DetailsLibro(int idlibro)
        {
            Libro libro = this.repo.FindLibro(idlibro);

            return View(libro);
        }

        
      
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
