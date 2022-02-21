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

    


    public class VistaPedidosController : Controller
    {
        private LibrosRepository repo;

        public VistaPedidosController(LibrosRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult VistaPedido()
        {
            int id = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<VistaPedidos> pedidos = this.repo.GetResumenPedidos(id);
            
            return View(pedidos);
        }
    }
}
