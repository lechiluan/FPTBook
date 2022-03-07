namespace FPTBook.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedback")]
    public partial class Feedback
    {
        [Key]
        [Display(Name = "Feedback ID")]
        public int FeedbackID { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Telephone")]
        [StringLength(20)]
        public string Telephone { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Message")]
        [StringLength(1000)]
        public string Message { get; set; }

        public DateTime DateSend { get; set; }

        public virtual Account Account { get; set; }
    }
}
