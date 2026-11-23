using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AppOptativa.Models
{
    public class Categorias
    {
        [Key]
        public int id_categoria { get; set; }
        
        public string? nombre { get; set; }
        
        public string? descripcion { get; set; }
    }
}