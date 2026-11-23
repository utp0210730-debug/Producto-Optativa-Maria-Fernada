using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AppOptativa.Models
{
    public class Proveedor
    {
        [Key]
        public int id_proveedor { get; set; }
        public string? nombre_empresa { get; set; }
        public string? contacto_nombre { get; set; }
        public string? telefono { get; set; }
        public string? email { get; set; }
        public string? direccion { get; set; }
    }
}