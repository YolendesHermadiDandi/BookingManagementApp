using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_educations")]
    public class Education : GlobalMathod
    {
        [Column("major", TypeName = "nvarchar(100)")]
        public string Major { get; set; }
        [Column("degree", TypeName = "nvarchar100")]
        public string Degree { get; set; }
        [Column("gpa", TypeName = "real")]
        public float Gpa { get; set; }
        [Column("university_guid", TypeName = "uniqueidentifier")]
        public Guid UniversityGuid { get; set; }
       
    }
}
