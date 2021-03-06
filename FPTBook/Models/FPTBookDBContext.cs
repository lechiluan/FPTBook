using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FPTBook.Models
{
    public partial class FPTBookDBContext : DbContext
    {
        public FPTBookDBContext()
            : base("name=FPTBook")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Feedbacks)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Author>()
                .Property(e => e.AuthorID)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .Property(e => e.AuthorName)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.BookID)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.CategoryID)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.AuthorID)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.PublisherID)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.OrderDetail)
                .WithRequired(e => e.Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryID)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Feedback>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Feedback>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Feedback>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Feedback>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.BookID)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.DeliveyAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Publisher>()
                .Property(e => e.PublisherID)
                .IsUnicode(false);

            modelBuilder.Entity<Publisher>()
                .Property(e => e.PublisherName)
                .IsUnicode(false);

            modelBuilder.Entity<Publisher>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Publisher>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Publisher)
                .WillCascadeOnDelete(false);
        }
    }
}
