using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppOptativa.Data;
using AppOptativa.Models;

namespace AppOptativa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categorias>>> GetCategorias()
        {
            return await _context.Categorias
                .OrderBy(c => c.id_categoria)
                .ToListAsync();
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categorias>> GetCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound("No se encontró la categoría.");
            }

            return categoria;
        }

        // POST: api/Categorias
        [HttpPost]
        public async Task<ActionResult<Categorias>> PostCategoria(Categorias categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCategoria),
                new { id = categoria.id_categoria },
                categoria
            );
        }

        // PUT: api/Categorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(
            int id,
            Categorias categoria)
        {
            if (id != categoria.id_categoria)
            {
                return BadRequest("El ID de la categoría no coincide.");
            }

            var categoriaExistente =
                await _context.Categorias.FindAsync(id);

            if (categoriaExistente == null)
            {
                return NotFound("No se encontró la categoría.");
            }

            categoriaExistente.nombre = categoria.nombre;
            categoriaExistente.descripcion = categoria.descripcion;

            await _context.SaveChangesAsync();

            return Ok(categoriaExistente);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria =
                await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound("No se encontró la categoría.");
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok("Categoría eliminada correctamente.");
        }
    }
}