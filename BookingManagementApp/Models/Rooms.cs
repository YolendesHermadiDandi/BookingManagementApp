namespace API.Models
{
    public class Rooms : GlobalMathod
    {

        public override Guid Guid
        {
            get => base.Guid;
            set => base.Guid = value;
        }
        public override string Name
        {
            get => base.Name;
            set => base.Name = value;
        }
        public int Floor { get; set; }
        public int Capacity { get; set; }
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
