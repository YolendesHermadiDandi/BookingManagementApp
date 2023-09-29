using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_roles")]
    public class Roles : GlobalMathod
    {

        [Column("name", TypeName = "nvarchar(100)")]
        public String Name { get; set; }

        // Cardinality
        public ICollection<AccountRoles>? AccountRoles { get; set; }

    }
}
