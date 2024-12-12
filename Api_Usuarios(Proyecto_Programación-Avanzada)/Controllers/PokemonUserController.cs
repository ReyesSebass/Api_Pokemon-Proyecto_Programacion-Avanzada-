using Api_Usuarios_Proyecto_Programación_Avanzada_.Data;
using Api_Usuarios_Proyecto_Programación_Avanzada_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonUserController : Controller
    {
        private readonly ConexionDbContext _contextAcceso;

        public PokemonUserController(ConexionDbContext contextAcceso)
        {
            _contextAcceso = contextAcceso;
        }

        [HttpGet ("Obtener todos los pokemones del usuario")]
        public ActionResult<IEnumerable<UsuarioModel>> ObtenerPokemonUsuario()
        {
            return Ok(_contextAcceso.G8_Pokemon_Users.ToList());
        }

        [HttpGet("Obtener pokemon del usuario por ID")]
        public ActionResult<IEnumerable<UsuarioModel>> ObtenerPokemonUsuarioID(int _id)
        {
            var datos = _contextAcceso.G8_Pokemon_Users.Find(_id);

            if (datos == null)
            {
                return NotFound("El dato buscado no existe.");
            }

            return Ok(datos);
        }

        [HttpPost("Agregar Pokemon al Usuarios")]
        public IActionResult AgregarPokemonUser(PokemonUsersModel pokemonUser)
        {
            try
            {
                // Verificar si el Pokémon existe en la Pokédex
                var pokemon = _contextAcceso.G8_Pokedex.FirstOrDefault(p => p.pokemon_id == pokemonUser.pokemon_id.pokemon_id);
                if (pokemon == null)
                {
                    return BadRequest("El Pokémon no está registrado en la Pokédex.");
                }

                // Agregar el Pokémon al usuario si todo es válido
                _contextAcceso.G8_Pokemon_Users.Add(pokemonUser);
                _contextAcceso.SaveChanges();

                return Ok("Tienes un nuevo pokemon");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut ("Editar Pokemon de Usuario")]
        public IActionResult ModificarPokemonUsuario(PokemonUsersModel _datos)
        {

            try
            {
                if (!ConsultarDatos(_datos.pokemon_user_id))
                {
                    return NotFound("El dato buscado no existe.");
                }

                _contextAcceso.Entry(_datos).State = EntityState.Modified;
                _contextAcceso.SaveChanges();

                return Ok("Pokemon actualizado");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpDelete("Eliminar Pokemon de Usuario")]
        public ActionResult EliminarPokemonUsuario(int _id)
        {
            try
            {
                if (!ConsultarDatos(_id))
                {
                    return NotFound("El dato buscado no existe.");
                }

                var datos = _contextAcceso.G8_Pokemon_Users.Find(_id);

                _contextAcceso.G8_Pokemon_Users.Remove(datos);
                _contextAcceso.SaveChanges();

                return Ok("Se elimino el pokemon del usuario");
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        // Funcion para verificar si exise 
        private bool ConsultarDatos(int _id)
        {
            return _contextAcceso.G8_Pokemon_Users.Any(x => x.pokemon_user_id == _id);
        }
    }
}
