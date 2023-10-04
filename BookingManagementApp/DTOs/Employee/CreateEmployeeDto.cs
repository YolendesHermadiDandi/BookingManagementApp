using API.DTOs.Educations;
using API.Models;
using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs.Employee
{
    public class CreateEmployeeDto
    {

        //setter getter
        //public string Nik { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        /* method implicit yang digunaakan untuk create Account Role
        * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
        *
        */
        public static implicit operator Employees(CreateEmployeeDto createEmployeeDto)
        {
            return new Employees
            {
                //Nik = createEmployeeDto.Nik,
                FirstName = createEmployeeDto.FirstName,
                LastName = createEmployeeDto.LastName,
                BirthDate = createEmployeeDto.BirthDate,
                Gender = createEmployeeDto.Gender,
                HiringDate = createEmployeeDto.HiringDate,
                Email = createEmployeeDto.Email,
                PhoneNumber = createEmployeeDto.PhoneNumber,
                CreateDate = DateTime.Now,
                ModifiedeDate = DateTime.Now,
            };
        }
    }
}
