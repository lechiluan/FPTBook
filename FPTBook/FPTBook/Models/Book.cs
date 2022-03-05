namespace FPTBook.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            Order_Details = new HashSet<Order_Details>();
        }

        [StringLength(10)]
        public string BookID { get; set; }

        [Required]
        [StringLength(100)]
        public string BookName { get; set; }

        [Required]
        [StringLength(10)]
        public string CategoryID { get; set; }

        [Required]
        [StringLength(10)]
        public string AuthorID { get; set; }

        [Required]
        [StringLength(10)]
        public string PublisherID { get; set; }

        public long Price { get; set; }

        public int Quantity { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public virtual Author Author { get; set; }

        public virtual Caterory Caterory { get; set; }

        public virtual Publisher Publisher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }
    }
}
