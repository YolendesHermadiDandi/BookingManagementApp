namespace API.Models
{
    public class Bookings : GlobalMathod
    {
        public override Guid Guid
        {
            get => base.Guid;
            set => base.Guid = value;
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }
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
