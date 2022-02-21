using Microsoft.AspNetCore.Http;
using PracticaCore2ART.Data;
using PracticaCore2ART.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2ART.Repositories
{
    public class LibrosRepository
    {

        private LibrosContext context;

        public LibrosRepository(LibrosContext context)
        {
            this.context = context;
        }

        public List<Libro> GetLibros(int posicion, ref int numerolibros)
        {
            var consulta = from datos in this.context.Libros
                           select datos;

            numerolibros = consulta.Count();

            var results = consulta.Skip(posicion).Take(5).ToList();
            return results;
        }

        public List<Genero> GetGeneros()
        {
            var consulta = from datos in this.context.Generos
                           select datos;

            return consulta.ToList();
        }

        public List<Libro> GetLibrosGenero(int idgenero)
        {
            var consulta = from datos in this.context.Libros
                           where datos.Id_Genero == idgenero
                           select datos;

            return consulta.ToList();
        }


        public Libro FindLibro(int idlibro)
        {
            return this.context.Libros
                .SingleOrDefault(x => x.Id_Libro == idlibro);
        }

        public List<Libro> GetLibrosSession(List<int> idslibros)
        {
            var consulta = from datos in this.context.Libros
                           where idslibros.Contains(datos.Id_Libro)
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return consulta.ToList();
            }
        }

        public int FindMaximoIdPedido()
        {
            return this.context.Pedidos.Max(x => x.Id_Pedido);
        }

        public int FindMaximaFactura()
        {
            return this.context.Pedidos.Max(x => x.Id_Factura);
        }

        public void InsertPedido(List<int> idlibro, int id)
        {
            int factura = this.FindMaximaFactura()+1;

            foreach (int i in idlibro)
            {
                DateTime Fecha = DateTime.Now;
                Pedido pedido = new Pedido();
                pedido.Id_Pedido = this.FindMaximoIdPedido() + 1;
                pedido.Id_Factura = factura;
                pedido.Fecha = Fecha;
                pedido.Id_Libro = i;
                pedido.Id_Usuario = id;
                pedido.Cantidad = 1;
                this.context.Pedidos.Add(pedido);
                this.context.SaveChanges();
            }
        }

        public User ExisteUser(string email, string password)
        {
            var consulta = from datos in this.context.Users
                           where datos.Email == email && datos.Password == password
                           select datos;

            return consulta.FirstOrDefault();
        }

        public User FindUser(int idsuer)
        {
            return this.context.Users
              .SingleOrDefault(x => x.Id_Usuario == idsuer);
        }

        public List<VistaPedidos> GetResumenPedidos(int idusuario)
        {
            var consulta = from datos in this.context.VistaPedidos
                           where datos.Id_User == idusuario
                           select datos;

            return consulta.ToList();
        }
    }
}
