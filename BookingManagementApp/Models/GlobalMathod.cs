using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    /* 
   * Generic class digunakan untuk membuat parameter/method untuk 
   * nama model dan tipedata yang sama 
   * dan diterapkan ke semua mode
   * class ini juga merupakan class parent dari semua class model
   * 
   * 
   */
    public abstract class GlobalMathod
    {

        [Key, Column("guid")]
        public Guid Guid { get; set; }
        [Column("create_date")]
        public DateTime CreateDate { get; set;a }
        [Column("modified_date")]
        public DateTime ModifiedeDate { get; set; }
    }
}
