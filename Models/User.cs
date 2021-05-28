using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parkyou.Models
{
    [Table("users")]
    public class User : ApplicationUser
    {
        public virtual ParkingSpot ParkSpot { get; set; }
    }
}