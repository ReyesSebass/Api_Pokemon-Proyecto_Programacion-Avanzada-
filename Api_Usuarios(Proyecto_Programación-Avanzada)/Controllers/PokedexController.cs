using Microsoft.AspNetCore.Mvc;

namespace Api_Usuarios_Proyecto_Programación_Avanzada_.Controllers
{
    public class PokedexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
