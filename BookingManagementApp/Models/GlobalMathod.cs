namespace API.Models
{
    public class GlobalMathod
    {

        public virtual string Name { get; set; }
        public virtual Guid Guid { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime ModifiedeDate { get; set; }
    }
}
