namespace API.Models
{
    public class Accounts : GlobalMathod
    {

        public string Password { get; set; }
        public Boolean IsDeleted { get; set; }
        public int OTP { get; set; }
        public Boolean IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }
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
