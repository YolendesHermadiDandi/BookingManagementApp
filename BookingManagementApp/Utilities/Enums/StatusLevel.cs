using System.ComponentModel.DataAnnotations;

namespace API.Utilities.Enums
{
    /*
    * pembuatan emun untuk status level
    * dengan reprentasi menggunakan angka dimulai dari 0
    */
    public enum StatusLevel
    {
        Requested,
        Approved,
        Rejected,
        Canceled,
        Completed,
        [Display(Name = "On Going")] Ongoing
    }
}
