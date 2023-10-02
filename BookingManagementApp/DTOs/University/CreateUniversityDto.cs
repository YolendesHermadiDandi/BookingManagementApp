using API.Models;

namespace API.DTOs.University
{
    public class CreateUniversityDto
    {
        //setter getter
        public string Code { get; set; }
        public string Name { get; set; }

        /*
         * method implicit yang digunaakan untuk create university
         * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
         * 
         */
        public static implicit operator Universities(CreateUniversityDto createUniversityDTO)
        {
            return new Universities
            {
                Code = createUniversityDTO.Code,
                Name = createUniversityDTO.Name,
                CreateDate = DateTime.Now,
                ModifiedeDate = DateTime.Now,
            };
        }
    }
}
