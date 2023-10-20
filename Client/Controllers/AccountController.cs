using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IAccountRepository repository;

        //public AccountController(IAccountRepository repository)
        //{
        //    this.repository = repository;
        //}

        //login 
        public IActionResult Login()
        {
            return View();
        }
    }
}
