using Api_Usuarios_Proyecto_Programación_Avanzada_.Data;
using Api_Usuarios_Proyecto_Programación_Avanzada_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly ConexionDbContext _contextAcceso;

        public UsuariosController(ConexionDbContext contextAcceso)
        {
            _contextAcceso = contextAcceso;
        }

        //Mostrar todos los usuarios existentes
        [HttpGet]
        public ActionResult<IEnumerable<UsuarioModel>> ObtenerUsuarios()
        {
            return Ok(_contextAcceso.users.ToList());
        }

        // Obtener un usuario por ID
        [HttpGet("{_id}")]
        public ActionResult<IEnumerable<UsuarioModel>> ObtenerUsuarios(int _id)
        {
            var datos = _contextAcceso.users.Find(_id);

            if (datos == null)
            {
                return NotFound("El dato buscado no existe.");
            }

            return Ok(datos);
        }

        //Agregar un Nuevo Usuario
        [HttpPost]
        public IActionResult AgregarUsuario(UsuarioModel _datos)
        {
            try
            {
                _contextAcceso.users.Add(_datos);
                _contextAcceso.SaveChanges();

                return Ok("Usuario insertado exitosamente.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Editar/Modificar un usuario existente
        [HttpPut]
        public IActionResult ModificarUsuario(UsuarioModel _datos)
        {
            try
            {
                if (!ConsultarDatos(_datos.user_id))
                {
                    return NotFound("El dato buscado no existe.");
                }
                _contextAcceso.Entry(_datos).State = EntityState.Modified;
                _contextAcceso.SaveChanges();

                return Ok("Usuario modificado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Eliminar un usuarios
        [HttpDelete("{_id}")]
        public ActionResult EliminarUsuarios(int _id)
        {
            try
            {
                if (!ConsultarDatos(_id))
                {
                    return NotFound("El dato buscado no existe.");
                }
                var datos = _contextAcceso.users.Find(_id);
                _contextAcceso.users.Remove(datos);
                _contextAcceso.SaveChanges();

                return Ok($"Se elimino el registro {_id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // Funcion para verificar si exise 
        private bool ConsultarDatos(int _id)
        {
            return _contextAcceso.users.Any(x => x.user_id == _id);
        }
    }
}
