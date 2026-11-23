using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppOptativa.Data;
using AppOptativa.Models;

namespace AppOptativa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productos>>> GetProductos()
        {
            return await _context.Productos
        .Include(p => p.Proveedor)
        .Include(p => p.Categoria)
        .OrderBy(p => p.id_producto)
        .ToListAsync();
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Productos>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        [HttpPost]
        public async Task<ActionResult<Productos>> PostProducto(Productos producto)
        {
            producto.stock_minimo = 0;

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProducto),
                new { id = producto.id_producto },
                producto
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Productos producto)
        {
            if (id != producto.id_producto)
            {
                return BadRequest("El ID del producto no coincide.");
            }

            var productoExistente = await _context.Productos.FindAsync(id);

            if (productoExistente == null)
            {
                return NotFound("No se encontró el producto.");
            }

            productoExistente.codigo_barras = producto.codigo_barras;
            productoExistente.nombre = producto.nombre;
            productoExistente.id_categoria = producto.id_categoria;
            productoExistente.id_proveedor = producto.id_proveedor;
            productoExistente.precio_compra = producto.precio_compra;
            productoExistente.precio_venta = producto.precio_venta;
            productoExistente.stock_actual = producto.stock_actual;

            await _context.SaveChangesAsync();

            return Ok(productoExistente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound("No se encontró el producto.");
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok("Producto eliminado correctamente.");
        }

        [HttpPost("movimiento")]
        public async Task<IActionResult> RegistrarMovimiento(
            int id_producto,
            string tipo,
            int cantidad)
        {
            var producto = await _context.Productos.FindAsync(id_producto);

            if (producto == null)
            {
                return NotFound("No se encontró el producto.");
            }

            if (cantidad <= 0)
            {
                return BadRequest("La cantidad debe ser mayor a 0.");
            }

            if (tipo == "ENTRADA")
            {
                producto.stock_actual = (producto.stock_actual ?? 0) + cantidad;
            }
            else if (tipo == "SALIDA")
            {
                if ((producto.stock_actual ?? 0) < cantidad)
                {
                    return BadRequest("No hay suficiente stock para realizar la salida.");
                }

                producto.stock_actual = (producto.stock_actual ?? 0) - cantidad;
            }
            else if (tipo == "AJUSTE")
            {
                producto.stock_actual = cantidad;
            }
            else
            {
                return BadRequest("El tipo de movimiento no es válido.");
            }

            await _context.SaveChangesAsync();

            return Ok(producto);
        }
    }
}