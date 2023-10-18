using API.DTOs.Employee;
using API.Models;
using API.Utilities.Handler;
using Client.Contracts;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Client.Repositories
{
    public class EmployeeRepository : GeneralRepository<EmployeeDto, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(string request = "employee/") : base(request)
        {

        }

        public async Task<ResponseOkHandler<EmployeeDto>> InsertEmployee(CreateEmployeeDto entity)
        {
            ResponseOkHandler<EmployeeDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "insert", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<EmployeeDto>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseOkHandler<EmployeeDto>> UpdateEmployee(EmployeeDto entity)
        {
            ResponseOkHandler<EmployeeDto> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PutAsync(request +"update", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseOkHandler<EmployeeDto>>(apiResponse);
            }
            return entityVM;
        }
    }
}
