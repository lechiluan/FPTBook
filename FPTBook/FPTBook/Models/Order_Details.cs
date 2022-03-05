namespace FPTBook.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_Details
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string BookID { get; set; }

        public int Quantity { get; set; }

        public long Price { get; set; }

        public long Subtotal { get; set; }

        public virtual Book Book { get; set; }

        public virtual Order Order { get; set; }
    }
}
