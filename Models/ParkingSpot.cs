using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parkyou.Models
{
    [Table("parking_spots")]
    public class ParkingSpot
    {
        public int Id { get; set; }
        [Required]
        [Column("Row")]
        [Display(Name = "Row")]
        public string Row { get; set; }
        [Required]
        [Column("Col")]
        [Display(Name = "Column")]
        public int Col { get; set; }
        [Required]
        [Column("Status")]
        [Display(Name = "Status")]
        public int Status { get; set; }
        [Required]
        [Column("UserName")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
    }
}