using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_employees")]
    public class Employees : GlobalMathod
    {

        [Column("nik", TypeName = "nchar(6)")]
        public char Nik { get; set; }
        [Column("first_name", TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        [Column("last_name", TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }
        [Column("birth_date", TypeName = "datetime2")]
        public DateTime BirthDate { get; set; }
        [Column("gender", TypeName = "int")]
        public GenderLevel Gender { get; set; }
        [Column("hiring_date", TypeName = "datetime2")]
        public DateTime HiringDate { get; set; }
        [Column("email", TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        [Column("phone_number", TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }

        //cardinality
        public Accounts? Accounts { get; set; }
        public ICollection<Bookings>? bookings { get; set; }
        public Education? Education { get; set; }

    }
}
