using API.DTOs.Educations;
using API.DTOs.Employee;
using API.DTOs.University;
using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Account
{
    public class RegisterAccoutDto
    {

        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate {  get; set; }
        public GenderLevel Gender {  get; set; }
        public DateTime HiringDate {  get; set; }
        public string Email { get; set; }
        public string PhoneNumber {  get; set; }
        public string Major {  get; set; }
        public string Degree {  get; set; }
        public float Gpa {  get; set; }
        public string UniversityCode { get; set; }
        public string UniversityName { get; set; }
        public string Password {  get; set; }
        public string ConfirmPassword {  get; set; }

        public static implicit operator Employees(RegisterAccoutDto createEmployeeDto)
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

        public static implicit operator Accounts(RegisterAccoutDto createAccountDto)
        {
            return new Accounts
            {
                Guid = Guid.NewGuid(),
                Password = createAccountDto.Password,
                CreateDate = DateTime.Now,
                ModifiedeDate = DateTime.Now,
            };
        }

        public static implicit operator Universities(RegisterAccoutDto createUniversityDTO)
        {
            return new Universities
            {
                Code = createUniversityDTO.UniversityCode,
                Name = createUniversityDTO.UniversityName,
                CreateDate = DateTime.Now,
                ModifiedeDate = DateTime.Now,
            };
        }

        public static implicit operator Education(RegisterAccoutDto createAccountRoleDto)
        {
            return new Education
            {
                Guid = Guid.NewGuid(),
                Major = createAccountRoleDto.Major,
                Degree = createAccountRoleDto.Degree,
                Gpa = createAccountRoleDto.Gpa,
                UniversityGuid = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                ModifiedeDate = DateTime.Now,
            };
        }



    }
}
