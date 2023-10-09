using API.DTOs.Booking;
using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Educations
{
    public class EducationDto
    {

        //setter getter
        public Guid Guid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public Guid UniversityGuid { get; set; }


        /*
        * Method explicit digunakan supaya ketika melakukan konversi data
        * perlu melakukan casting (penjelasan dari tipe data yang akan di konvert)
        * sehingga jika ingin melakukan konversi data perlu melakukan casting
        * supaya memasukan data yang dikonvert itu benar.
        * 
        */
        public static explicit operator EducationDto(Education education)
        {
            return new EducationDto
            {
                Guid = education.Guid,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.Gpa,
                UniversityGuid = education.UniversityGuid
            };
        }

        /*
         * method implicit yang digunaakan untuk create
         * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
         * 
         */
        public static implicit operator Education(EducationDto educationDto)
        {
            return new Education
            {
                Guid = educationDto.Guid,
                Major = educationDto.Major,
                Degree = educationDto.Degree,
                Gpa = educationDto.Gpa,
                UniversityGuid = educationDto.UniversityGuid,
                ModifiedeDate = DateTime.Now,
            };
        }
    }
}
