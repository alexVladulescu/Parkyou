using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Parkyou.Models
{
    public class AuthenticationModel
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
        public bool KeepLoggedIn { get; set; }
        public virtual int FailedLastLogin { get; set; }
    }
}