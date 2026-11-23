using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppOptativa.Data;
using AppOptativa.Models;

namespace AppOptativa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetRoles()
        {
            return await _context.Roles
                .OrderBy(r => r.id_rol)
                .ToListAsync();
        }

        // GET: api/Roles/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetRol(int id)
        {
            var rol = await _context.Roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound("No se encontró el usuario.");
            }

            return rol;
        }

        // POST: api/Roles/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var usuario = await _context.Roles
                .FirstOrDefaultAsync(r =>
                    r.usuario == login.usuario &&
                    r.contraseña == login.contraseña);

            if (usuario == null)
            {
                return Unauthorized(new
                {
                    mensaje = "Usuario o contraseña incorrectos."
                });
            }

            return Ok(new
            {
                id_rol = usuario.id_rol,
                tipo_rol = usuario.tipo_rol,
                usuario = usuario.usuario
            });
        }
    }

    public class LoginRequest
    {
        public string? usuario { get; set; }
        public string? contraseña { get; set; }
    }
}