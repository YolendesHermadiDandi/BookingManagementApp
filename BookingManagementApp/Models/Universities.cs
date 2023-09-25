namespace API.Models
{
    public class Universities : GlobalMathod
    {

        public string Code { get; set; }
        public override string Name
        {
            get => base.Name;
            set => base.Name = value;
        }
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
