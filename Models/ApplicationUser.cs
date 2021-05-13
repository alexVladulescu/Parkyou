using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Parkyou.Models
{
    public class ApplicationUser : IdentityUser
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("Id")]
        [Display(Name = "Id")]
        [MaxLength(20)]
        public override string Id { get; set; }
        [Required]
        [StringLength(50)]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Prenumele nu poate fi mai lung de 50 de caractere.")]
        [Column("LastName")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Column("Rol")]
        [Display(Name = "Rol")]
        public string Rol { get; set; }
    }
}