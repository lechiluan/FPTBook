namespace FPTBook.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Order_Details = new HashSet<Order_Details>();
        }

        public int OrderID { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Telephone")]
        [StringLength(20)]
        public string Telephone { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [StringLength(50)]
        public string Fullname { get; set; }

        [Required]
        [Display(Name = "Delivery Address")]
        [StringLength(100)]
        public string DeliveyAddress { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Total Price")]
        public int TotalPrice { get; set; }

        [Required]
        [Display(Name = "Payment ID")]
        [StringLength(10)]
        public string PaymentID { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
