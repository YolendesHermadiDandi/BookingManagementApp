using API.DTOs.Employee;
using API.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Client.Controllers
{
    //[Authorize]
    [Authorize(Roles = "admin, manager")]

    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<JsonResult> GetAll()
        {
            var result = await repository.Get();
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Insert(CreateEmployeeDto createEmployeeDto)
        {
            var result = await repository.InsertEmployee(createEmployeeDto);
            if (result.Code == 200)
            {
                return Json(result);    
            }
            else if (result.Code == 400)
            {
               return Json(result);
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Update(EmployeeDto emp)
        {
            var result = await repository.UpdateEmployee(emp);

            if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Updated - {result.Message}!";
                return Json(result);
            }
            return Json(result);
        }

        [Route("employee/edit/{guid}")]
        public async Task<JsonResult> Edit(Guid guid)
        {
            var result = await repository.Get(guid);
            var employee = new EmployeeDto();

            if (result.Data != null)
            {
                employee = (EmployeeDto)result.Data;
            }
            return Json(employee);
        }
        
        [Route("employee/delete/{guid}")]
        public async Task<JsonResult> Delete(Guid guid)
        {
            var result = await repository.Delete(guid);

            if (result.Code == 500)
            {
                TempData["Success"] = $"Data has been Successfully Updated - {result.Message}!";
                return Json(result);
            }
            if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Updated - {result.Message}!";
                return Json(result);
            }
            return Json(result);

        }

        
        




    }
}
