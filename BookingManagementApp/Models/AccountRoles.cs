using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_account_roles")]
    public class AccountRoles : GlobalMathod
    {
        [Column("account_guid", TypeName = "uniqueidentifier")]
        public Guid AccountGuid { get; set; }
        [Column("role_guid", TypeName = "uniqueidentifier")]
        public Guid RoleGuid { get; set; }

        // Cardinality
        public Roles? Roles { get; set; }
        public Accounts? Accounts { get; set; }

    }
}
