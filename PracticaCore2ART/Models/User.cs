using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2ART.Models
{
    [Table("USUARIOS")]
    public class User
    {
        [Key]
        [Column("IdUsuario")]
        public int Id_Usuario { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Apellidos")]
        public string Apellidos { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Pass")]
        public string Password { get; set; }
        [Column("Foto")]
        public string Foto { get; set; }
    }
}
