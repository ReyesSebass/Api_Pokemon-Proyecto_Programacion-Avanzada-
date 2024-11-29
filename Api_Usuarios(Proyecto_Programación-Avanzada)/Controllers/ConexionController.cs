using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConexionController : Controller
    {

        private readonly IConfiguration _configuration;

        public ConexionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Funciones
        [HttpGet]
        public ActionResult Conectar()
        {
            string connetionString = _configuration.GetConnectionString("ConexionBD");

            //Trycatch para comprobación de caidas del sistema
            try
            {
                using (var conexion = new MySqlConnection(connetionString))
                {
                    conexion.Open();
                    return Ok("Conexion Exitosa");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
