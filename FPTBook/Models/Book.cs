namespace FPTBook.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Book")]
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        [Key]
        [Required]
        [StringLength(10)]
        [Display(Name = "Book ID")]
        public string BookID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Book Name")]
        public string BookName { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Category ID")]
        public string CategoryID { get; set; }

        [Required]
        [Display(Name = "Author ID")]
        public string AuthorID { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Publisher ID")]
        public string PublisherID { get; set; }

        [Required]
        [Display(Name = "Price")]
        public long Price { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }


        [DataType(DataType.Upload)]
        [Display(Name = "Image")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public virtual Author Author { get; set; }

        public virtual Category Category { get; set; }

        public virtual Publisher Publisher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
