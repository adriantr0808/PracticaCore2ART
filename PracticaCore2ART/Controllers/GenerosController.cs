using Microsoft.AspNetCore.Mvc;
using PracticaCore2ART.Models;
using PracticaCore2ART.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2ART.Controllers
{
    public class GenerosController : Controller
    {
        private LibrosRepository repo;
        public GenerosController(LibrosRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult LibrosGenero(int idgenero)
        {
            List<Libro> libros = this.repo.GetLibrosGenero(idgenero);
            return View(libros);
        }
    }
}
