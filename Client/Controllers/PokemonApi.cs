using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class PokemonApi : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
