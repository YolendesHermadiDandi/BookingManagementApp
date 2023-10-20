using API.DTOs.Account;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAccountRepository repository;

        public AuthController(IAccountRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SignIn(LoginAccountDto loginAccountDto)
        {
            var result = await repository.Login(loginAccountDto);


            if (result is null)
            {
                return Json(result);
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return Json(result);
            }
            else if (result.Code == 200)
            {
                HttpContext.Session.SetString("JWToken", result.Data.Token);
                return Json(result);
            }
            return Json(result);
        }

        
        [HttpGet("Logout/")]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "Home");
        }
    }
}
