using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class PokemonApi : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
