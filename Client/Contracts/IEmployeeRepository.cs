using API.DTOs.Employee;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace Client.Contracts
{
    public interface IEmployeeRepository: IRepository<EmployeeDto, Guid>
    {
        Task<ResponseOkHandler<EmployeeDto>> InsertEmployee(CreateEmployeeDto entity);
        Task<ResponseOkHandler<EmployeeDto>> UpdateEmployee(EmployeeDto entity);
    }
}
