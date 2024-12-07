using Api_Usuarios_Proyecto_Programación_Avanzada_.Data;
using Api_Usuarios_Proyecto_Programación_Avanzada_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Controllers
{
    public class PokedexController : Controller
    {
        private readonly ConexionDbContext _contextAcceso;

        public PokedexController(ConexionDbContext contextAcceso)
        {
            _contextAcceso = contextAcceso;
        }

        //Mostrar todos los pokemones existentes
        [HttpGet("getAllPokemon")]
        public ActionResult<IEnumerable<PokedexModel>> ObtenerTodoslosPokemones()
        {
            return Ok(_contextAcceso.pokedex.ToList());
        }

        // Obtener un pokemon por ID
        [HttpGet("searchPokemon by {_id}")]
        public ActionResult<IEnumerable<PokedexModel>> ObtenerPokemon(int _id)
        {
            var datos = _contextAcceso.pokedex.Find(_id);

            if (datos == null)
            {
                return NotFound("El pokemon buscado no existe.");
            }

            return Ok(datos);
        }

        //Agregar un Nuevo Pokemon
        [HttpPost("addPokemon")]
        public IActionResult AgregarPokemon(PokedexModel _datos)
        {
            try
            {
                _contextAcceso.pokedex.Add(_datos);
                _contextAcceso.SaveChanges();

                return Ok("Se registro un nuevo pokemon en la pokedex");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Editar/Modificar un pokemon existente
        [HttpPut("editPokemon")]
        public IActionResult ModificarPokemon(PokedexModel _datos)
        {
            try
            {
                if (!ConsultarDatos(_datos.pokemon_id))
                {
                    return NotFound("El pokemon buscado no existe.");
                }
                _contextAcceso.Entry(_datos).State = EntityState.Modified;
                _contextAcceso.SaveChanges();

                return Ok("Pokemon modificado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Eliminar un pokemon
        [HttpDelete("deletePokemon")]
        public ActionResult EliminarPokemon(int _id)
        {
            try
            {
                if (!ConsultarDatos(_id))
                {
                    return NotFound("El pokemon buscado no existe.");
                }
                var datos = _contextAcceso.pokedex.Find(_id);
                _contextAcceso.pokedex.Remove(datos);
                _contextAcceso.SaveChanges();

                return Ok($"Se elimino el pokemon con el id: {_id}, la base de datos");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // Funcion para verificar si exise 
        private bool ConsultarDatos(int _id)
        {
            return _contextAcceso.pokedex.Any(x => x.pokemon_id == _id);
        }
    }
}
