using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_rooms")]
    public class Rooms : GlobalMathod
    {

        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Column("floor", TypeName = "int")]
        public int Floor { get; set; }
        [Column("capacity", TypeName = "int")]
        public int Capacity { get; set; }


    }
}
