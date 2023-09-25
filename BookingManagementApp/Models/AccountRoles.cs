namespace API.Models
{
    public class AccountRoles : GlobalMathod
    {

        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }
        public override Guid Guid
        {
            get => base.Guid;
            set => base.Guid = value;
        }
        public override DateTime CreateDate
        {
            get => base.CreateDate;
            set => base.CreateDate = value;
        }
        public override DateTime ModifiedeDate
        {
            get => base.ModifiedeDate;
            set => base.ModifiedeDate = value;
        }
    }
}
