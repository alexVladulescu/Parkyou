using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parkyou.Models
{
    [Table("administrators")]
    public class Administrator : ApplicationUser
    {
    }
}