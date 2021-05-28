using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parkyou.Models
{
    [Table("reports")]
    public class Report
    {
        [Key]
        [Required]
        [Column("Id")]
        [Display(Name = "Id")]
        [StringLength(36)]
        public string Id { get; set; }
        [Required]
        [Column("Title")]
        [Display(Name = "Title")]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [Column("Description")]
        [Display(Name = "Description")]
        [StringLength(500)]
        public string Description { get; set; }
        [Column("Row")]
        [Display(Name = "Row")]
        public string Row { get; set; }
        [Column("Col")]
        [Display(Name = "Col")]
        public string Col { get; set; }
        [Required]
        [Column("ReportedBy")]
        [Display(Name = "Reported By")]
        public string ReportedBy { get; set; }
        [Column("Solved")]
        [Display(Name = "Solved")]
        public bool Solved { get; set; }
        [Column("Resolution")]
        [Display(Name = "Resolution")]
        [StringLength(500)]
        public string Resolution { get; set; }
        [Column("Closed")]
        [Display(Name = "Closed")]
        public bool Closed { get; set; }
        [Required]
        [Column("Created")]
        [Display(Name = "Created")]
        public DateTime Created { get; set; }
        [Column("LastModified")]
        [Display(Name = "Last Modified")]
        public DateTime? LastModified { get; set; }
    }
}