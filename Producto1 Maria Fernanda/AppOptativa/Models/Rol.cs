using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppOptativa.Models
{
    
    public class Rol
    {
        [Key]
        
        public int id_rol { get; set; }

       
        public string? tipo_rol { get; set; }

     
        public string? usuario { get; set; }

     
        public string? contraseña { get; set; }
    }
}