using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppOptativa.Data;
using AppOptativa.Models;

namespace AppOptativa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProveedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
        {
            return await _context.Proveedores
                .OrderBy(p => p.id_proveedor)
                .ToListAsync();
        }

        // GET: api/Proveedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound("No se encontró el proveedor.");
            }

            return proveedor;
        }

        // POST: api/Proveedores
        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProveedor),
                new { id = proveedor.id_proveedor },
                proveedor
            );
        }

        // PUT: api/Proveedores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(
            int id,
            Proveedor proveedor)
        {
            if (id != proveedor.id_proveedor)
            {
                return BadRequest("El ID del proveedor no coincide.");
            }

            var proveedorExistente =
                await _context.Proveedores.FindAsync(id);

            if (proveedorExistente == null)
            {
                return NotFound("No se encontró el proveedor.");
            }

            proveedorExistente.nombre_empresa = proveedor.nombre_empresa;
            proveedorExistente.contacto_nombre = proveedor.contacto_nombre;
            proveedorExistente.telefono = proveedor.telefono;
            proveedorExistente.email = proveedor.email;
            proveedorExistente.direccion = proveedor.direccion;

            await _context.SaveChangesAsync();

            return Ok(proveedorExistente);
        }

        // DELETE: api/Proveedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor =
                await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound("No se encontró el proveedor.");
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return Ok("Proveedor eliminado correctamente.");
        }
    }
}