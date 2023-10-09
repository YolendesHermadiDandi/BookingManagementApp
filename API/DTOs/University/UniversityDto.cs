using API.Models;

namespace API.DTOs.University
{
    public class UniversityDto
    {
        public Guid Guid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        /*
         * Method explicit digunakan supaya ketika melakukan konversi data
         * perlu melakukan casting (penjelasan dari tipe data yang akan di konvert)
         * sehingga jika ingin melakukan konversi data perlu melakukan casting
         * supaya memasukan data yang dikonvert itu benar.
         * 
         */
        public static explicit operator UniversityDto(Universities university)
        {
            return new UniversityDto
            {
                Guid = university.Guid,
                Code = university.Code,
                Name = university.Name
            };
        }

        /*
         * method implicit yang digunaakan untuk create university
         * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
         * 
         */
        public static implicit operator Universities(UniversityDto universityDto)
        {
            return new Universities
            {
                Guid = universityDto.Guid,
                Code = universityDto.Code,
                Name = universityDto.Name,
                ModifiedeDate = DateTime.Now
            };
        }
    }
}
