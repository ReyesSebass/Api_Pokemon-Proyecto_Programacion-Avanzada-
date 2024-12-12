using Api_Usuarios_Proyecto_Programación_Avanzada_.Data;
using Api_Usuarios_Proyecto_Programación_Avanzada_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonTeamController : Controller
    {
        private readonly ConexionDbContext _contextAcceso;

        public PokemonTeamController(ConexionDbContext contextAcceso)
        {
            _contextAcceso = contextAcceso;
        }

        [HttpGet ("Obtener todos los equipos pokemon")]
        public ActionResult<IEnumerable<PokemonTeamModel>> ObtenerPokemonTeam()
        {
            return Ok(_contextAcceso.G8_Pokemon_Team.ToList());
        }

        [HttpGet("Obtener equipo pokemon por ID")]
        public ActionResult<IEnumerable<PokemonTeamModel>> ObtenerPokemonTeamID(int _id)
        {
            var datos = _contextAcceso.G8_Pokemon_Team.Find(_id);

            if (datos == null)
            {
                return NotFound("El equipo que se busca no exite");
            }

            return Ok(datos);
        }

        [HttpPost ("Agregar pokemon team")]
        public IActionResult AgregarEquipo(PokemonTeamModel equipo)
        {
            try
            {
                // Verificar si el entrenador existe
                var entrenador = _contextAcceso.G8_Users.FirstOrDefault(u => u.user_id == equipo.entrenador_id.user_id);
                if (entrenador == null)
                {
                    return BadRequest("El entrenador no está registrado.");
                }

                // Verificar si los Pokémon existen y si pertenecen al entrenador
                var pokemon1 = _contextAcceso.G8_Pokemon_Users
                    .FirstOrDefault(pu => pu.pokemon_id.pokemon_id == equipo.pokemon1_id.pokemon_id && pu.user_id.user_id == equipo.entrenador_id.user_id);
                var pokemon2 = _contextAcceso.G8_Pokemon_Users
                    .FirstOrDefault(pu => pu.pokemon_id.pokemon_id == equipo.pokemon2_id.pokemon_id && pu.user_id.user_id == equipo.entrenador_id.user_id);
                var pokemon3 = _contextAcceso.G8_Pokemon_Users
                    .FirstOrDefault(pu => pu.pokemon_id.pokemon_id == equipo.pokemon3_id.pokemon_id && pu.user_id.user_id == equipo.entrenador_id.user_id);

                if (pokemon1 == null)
                {
                    return BadRequest("No tienes el Pokémon 1 en tu Pokédex.");
                }
                if (pokemon2 == null)
                {
                    return BadRequest("No tienes el Pokémon 2 en tu Pokédex.");
                }
                if (pokemon3 == null)
                {
                    return BadRequest("No tienes el Pokémon 3 en tu Pokédex.");
                }

                // Agregar el equipo si todo es válido
                _contextAcceso.G8_Pokemon_Team.Add(equipo);
                _contextAcceso.SaveChanges();

                return Ok("Equipo agregado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut ("Modificar Pokemon Team")]
        public IActionResult ModificarPokemonTeam(PokemonTeamModel _datos)
        {

            try
            {
                if (!ConsultarDatos(_datos.team_id))
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

        [HttpDelete("Eliminar Pokemon Team")]
        public ActionResult EliminarPokemonTeam(int _id)
        {
            try
            {
                if (!ConsultarDatos(_id))
                {
                    return NotFound("El dato buscado no existe.");
                }

                var datos = _contextAcceso.G8_Pokemon_Team.Find(_id);

                _contextAcceso.G8_Pokemon_Team.Remove(datos);
                _contextAcceso.SaveChanges();

                return Ok("Se elimino el equipo pokemon");
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        private bool ConsultarDatos(int _id)
        {
            return _contextAcceso.G8_Pokemon_Team.Any(x => x.team_id == _id);
        }

    }
}
