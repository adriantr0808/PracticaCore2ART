using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2ART.Models
{
    [Table("PEDIDOS")]
    public class Pedido
    {
        [Key]
        [Column("IDPEDIDO")]
        public int Id_Pedido { get; set; }
        [Column("IDFACTURA")]
        public int Id_Factura { get; set; }
        [Column("FECHA")]
        public DateTime Fecha { get; set; }
        [Column("IDLIBRO")]
        public int Id_Libro { get; set; }
        [Column("IDUSUARIO")]
        public int Id_Usuario { get; set; }
        [Column("CANTIDAD")]
        public int Cantidad { get; set; }
    }
}
