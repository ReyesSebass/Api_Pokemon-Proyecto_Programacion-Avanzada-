using Api_Usuarios_Proyecto_Programación_Avanzada_.Data;
using Api_Usuarios_Proyecto_Programación_Avanzada_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RetosController : Controller
    {
        private readonly ConexionDbContext _contextAcceso;

        public RetosController(ConexionDbContext contextAcceso)
        {
            _contextAcceso = contextAcceso;
        }

        [HttpGet ("Obtener todos los retos")]
        public ActionResult<IEnumerable<RetosModel>> ObtenerRetos()
        {
            return Ok(_contextAcceso.G8_Retos.ToList());
        }

        [HttpGet("Obtener reto por ID")]
        public ActionResult<IEnumerable<RetosModel>> ObtenerRetosID(int _id)
        {
            var datos = _contextAcceso.G8_Retos.Find(_id);

            if (datos == null)
            {
                return NotFound("El dato buscado no existe.");
            }

            return Ok(datos);
        }

        [HttpPost("Agregar Reto")]
        public IActionResult AgregarReto(RetosModel reto)
        {
            try
            {
                // Verificar si los entrenadores existen
                var entrenador1 = _contextAcceso.G8_Users.FirstOrDefault(e => e.user_id == reto.entrenador1_id.user_id);
                var entrenador2 = _contextAcceso.G8_Users.FirstOrDefault(e => e.user_id == reto.entrenador2_id.user_id);
                if (entrenador1 == null || entrenador2 == null)
                {
                    return BadRequest("Ambos entrenadores deben existir.");
                }

                // Verificar si los equipos pertenecen a los entrenadores
                var equipo1 = _contextAcceso.G8_Pokemon_Team.FirstOrDefault(e => e.team_id == reto.equipo1_id.team_id);
                var equipo2 = _contextAcceso.G8_Pokemon_Team.FirstOrDefault(e => e.team_id == reto.equipo2_id.team_id);
                if (equipo1 == null || equipo2 == null)
                {
                    return BadRequest("Los equipos no existen.");
                }
                if (equipo1.entrenador_id.user_id != reto.entrenador1_id.user_id)
                {
                    return BadRequest("El equipo 1 no pertenece al entrenador 1.");
                }
                if (equipo2.entrenador_id.user_id != reto.entrenador2_id.user_id)
                {
                    return BadRequest("El equipo 2 no pertenece al entrenador 2.");
                }

                // Agregar el reto si todo es válido
                reto.fecha_reto = DateTime.Now; // O la fecha que desees asignar
                reto.estado = "Pendiente"; // O el estado que desees asignar
                _contextAcceso.G8_Retos.Add(reto);
                _contextAcceso.SaveChanges();

                return Ok("Reto agregado con éxito");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut ("Modificar el Reto")]
        public IActionResult ModificarReto(RetosModel _datos)
        {

            try
            {
                if (!ConsultarDatos(_datos.reto_id))
                {
                    return NotFound("El Reto que busca, no exite");
                }

                _contextAcceso.Entry(_datos).State = EntityState.Modified;
                _contextAcceso.SaveChanges();

                return Ok("El reto fue actualizado exitosamente.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpDelete("Eliminar el Reto")]
        public ActionResult EliminarUsuarios(int _id)
        {
            try
            {
                if (!ConsultarDatos(_id))
                {
                    return NotFound("El Reto que busca, no exite");
                }

                var datos = _contextAcceso.G8_Retos.Find(_id);

                _contextAcceso.G8_Retos.Remove(datos);
                _contextAcceso.SaveChanges();

                return Ok("Se elimino el reto");
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }



        private bool ConsultarDatos(int _id)
        {
            return _contextAcceso.G8_Retos.Any(x => x.reto_id == _id);
        }
    }
}
