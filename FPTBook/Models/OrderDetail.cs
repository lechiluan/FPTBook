namespace FPTBook.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "Order ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [Display(Name = "Book ID")]
        [StringLength(10)]
        public string BookID { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Price")]
        public long Price { get; set; }

        [Display(Name = "Subtotal")]
        public long Subtotal { get; set; }

        public virtual Book Book { get; set; }

        public virtual Order Order { get; set; }
    }
}