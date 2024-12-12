using Api_Usuarios_Proyecto_Programación_Avanzada_.Data;
using Api_Usuarios_Proyecto_Programación_Avanzada_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ConexionDbContext _contextAcceso;

        public RolesController(ConexionDbContext contextAcceso)
        {
            _contextAcceso = contextAcceso;
        }

        //Mostrar todos los roles existentes
        [HttpGet("Obtener todos los roles")]
        public ActionResult<IEnumerable<RolModel>> ObtenerRol()
        {
            return Ok(_contextAcceso.G8_Roles.ToList());
        }

        // Obtener un rol por ID
        [HttpGet("Buscar rol por id")]
        public ActionResult<IEnumerable<UsuarioModel>> ObtenerRoles(int _id)
        {
            var datos = _contextAcceso.G8_Roles.Find(_id);

            if (datos == null)
            {
                return NotFound("El dato buscado no existe.");
            }

            return Ok(datos);
        }

        //Agregar un Nuevo Rol
        [HttpPost("Agregar Rol")]
        public IActionResult AgregarRol(RolModel _datos)
        {
            try
            {
                _contextAcceso.G8_Roles.Add(_datos);
                _contextAcceso.SaveChanges();

                return Ok("Rol insertado exitosamente.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Editar/Modificar un rol existente
        [HttpPut("Editar Rol")]
        public IActionResult ModificarRol(RolModel _datos)
        {
            try
            {
                if (!ConsultarDatos(_datos.rol_id))
                {
                    return NotFound("El dato buscado no existe.");
                }
                _contextAcceso.Entry(_datos).State = EntityState.Modified;
                _contextAcceso.SaveChanges();

                return Ok("Rol modificado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Eliminar un usuarios
        [HttpDelete("Eliminar Rol")]
        public ActionResult EliminarRol(int _id)
        {
            try
            {
                if (!ConsultarDatos(_id))
                {
                    return NotFound("El dato buscado no existe.");
                }
                var datos = _contextAcceso.G8_Roles.Find(_id);
                _contextAcceso.G8_Roles.Remove(datos);
                _contextAcceso.SaveChanges();

                return Ok($"Se elimino el registro {_id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private bool ConsultarDatos(int _id)
        {
            return _contextAcceso.G8_Roles.Any(x => x.rol_id == _id);
        }
    }
}
