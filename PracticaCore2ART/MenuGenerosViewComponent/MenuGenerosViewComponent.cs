using Microsoft.AspNetCore.Mvc;
using PracticaCore2ART.Models;
using PracticaCore2ART.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2ART.MenuGenerosViewComponent
{
    public class MenuGenerosViewComponent: ViewComponent
    {

        private LibrosRepository repo;

        public MenuGenerosViewComponent(LibrosRepository repo)
        {
            this.repo = repo;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> generos = this.repo.GetGeneros();

            return View(generos);
        }


    }
}
