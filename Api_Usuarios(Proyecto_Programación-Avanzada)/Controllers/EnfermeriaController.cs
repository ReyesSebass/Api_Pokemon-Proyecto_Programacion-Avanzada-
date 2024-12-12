using Api_Usuarios_Proyecto_Programación_Avanzada_.Data;
using Api_Usuarios_Proyecto_Programación_Avanzada_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnfermeriaController : Controller
    {
        private readonly ConexionDbContext _contextAcceso;

        public EnfermeriaController(ConexionDbContext contextAcceso)
        {
            _contextAcceso = contextAcceso;
        }

        [HttpGet("Obtener todas las peticiones")]
        public ActionResult<IEnumerable<EnfermeriaModel>> ObtenerPeticiones()
        {
            return Ok(_contextAcceso.G8_Peticiones_de_Enfermeria.ToList());
        }

        [HttpGet("Obtener peticion por ID")]
        public ActionResult<IEnumerable<EnfermeriaModel>> ObtenerPeticionID(int _id)
        {
            var datos = _contextAcceso.G8_Peticiones_de_Enfermeria.Find(_id);

            if (datos == null)
            {
                return NotFound("La peticion que buscas no existe");
            }

            return Ok(datos);
        }

        [HttpPost("Agregar Peticion de Enfermeria")]
        public IActionResult AgregarPeticion(EnfermeriaModel peticion)
        {
            try
            {
                // Verificar si la enfermera es enfermera
                var enfermera = _contextAcceso.G8_Users.FirstOrDefault(e => e.user_id == peticion.enfermera_id.user_id);
                if (enfermera == null || enfermera.rol_id.nombre_rol != "Enfermera")
                {
                    return BadRequest("La enfermera no es enfermera.");
                }

                // Verificar si el entrenador es entrenador
                var entrenador = _contextAcceso.G8_Users.FirstOrDefault(e => e.user_id == peticion.entrenador_id.user_id);
                if (entrenador == null || entrenador.rol_id.nombre_rol != "Entrenador")
                {
                    return BadRequest("El entrenador no es entrenador.");
                }

                // Verificar si el Pokémon es del entrenador
                // Verificar si el Pokémon es del entrenador
                var pokemon = _contextAcceso.G8_Pokemon_Users.FirstOrDefault(p => p.pokemon_id == peticion.pokemon_id && p.user_id.user_id == entrenador.user_id);
                if (pokemon == null)
                {
                    return BadRequest("El Pokémon no es del entrenador.");
                }

                // Agregar la peticion si todo es válido
                peticion.fecha_peticion = DateTime.Now; // O la fecha que desees asignar
                peticion.estado = "Pendiente"; // O el estado que desees asignar
                _contextAcceso.G8_Peticiones_de_Enfermeria.Add(peticion);
                _contextAcceso.SaveChanges();

                return Ok("Peticion agregada con éxito");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Modificar peticion de enfermeria")]
        public IActionResult ModificarPeticion(EnfermeriaModel _datos)
        {

            try
            {
                if (!ConsultarDatos(_datos.peticion_id))
                {
                    return NotFound("El id de la peticion no exite");
                }

                _contextAcceso.Entry(_datos).State = EntityState.Modified;
                _contextAcceso.SaveChanges();

                return Ok("Peticion modificado exitosamente.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpDelete("Elimnar peticion de enfermeria")]
        public ActionResult EliminarPeticion(int _id)
        {
            try
            {
                if (!ConsultarDatos(_id))
                {
                    return NotFound("El dato buscado no existe.");
                }

                var datos = _contextAcceso.G8_Peticiones_de_Enfermeria.Find(_id);

                _contextAcceso.G8_Peticiones_de_Enfermeria.Remove(datos);
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
            return _contextAcceso.G8_Peticiones_de_Enfermeria.Any(x => x.peticion_id == _id);
        }
    }
}
