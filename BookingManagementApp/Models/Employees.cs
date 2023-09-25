namespace API.Models
{
    public class Employees : GlobalMathod
    {
        public override Guid Guid
        {
            get => base.Guid;
            set => base.Guid = value;
        }
        public char Nik { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


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
