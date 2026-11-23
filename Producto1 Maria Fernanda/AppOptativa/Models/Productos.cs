using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppOptativa.Models
{
    public class Productos
    {
        [Key]
        public int id_producto { get; set; }
        public string? codigo_barras { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public int? id_categoria { get; set; }
        [ForeignKey(nameof(id_categoria))]
        public Categorias? Categoria { get; set; }
        public int? id_proveedor { get; set; }
        [ForeignKey(nameof(id_proveedor))]
        public Proveedor? Proveedor { get; set; }

        public decimal? precio_compra { get; set; }
        public decimal? precio_venta { get; set; }
        public int? stock_actual { get; set; }
        public int? stock_minimo { get; set; }
        public DateTime? fecha_creacion { get; set; }
    }
}