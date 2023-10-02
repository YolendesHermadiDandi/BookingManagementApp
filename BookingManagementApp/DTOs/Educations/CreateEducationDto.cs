using API.DTOs.AccountRole;
using API.Models;

namespace API.DTOs.Educations
{
    public class CreateEducationDto
    {
        //setter getter
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public Guid UniversityGuid { get; set; }

        /*
      * method implicit yang digunaakan untuk create Account Role
      * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
      * 
      */
        public static implicit operator Education(CreateEducationDto createAccountRoleDto)
        {
            return new Education
            {
                Major = createAccountRoleDto.Major,
                Degree = createAccountRoleDto.Degree,
                Gpa = createAccountRoleDto.Gpa,
                UniversityGuid = createAccountRoleDto.UniversityGuid,
                CreateDate = DateTime.Now,
                ModifiedeDate = DateTime.Now,
            };
        }
    }
}
