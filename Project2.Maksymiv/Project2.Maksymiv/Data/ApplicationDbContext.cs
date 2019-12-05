namespace Librairie.Data
{
    using Librairie.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<AuthorBook> AuthorBooks { get; set; }

        public DbSet<UserBook> UserBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(p => p.Id);
            modelBuilder.Entity<Book>().Property(p => p.Price).HasColumnType("money").IsRequired();
            modelBuilder.Entity<Book>().Property(p => p.Title).IsRequired();

            modelBuilder.Entity<AuthorBook>().HasKey(p => new { p.AuthorId, p.BookId });
            modelBuilder.Entity<AuthorBook>().HasOne(p => p.Book).WithMany(p => p.AuthorBooks).HasForeignKey(p => p.BookId);
            modelBuilder.Entity<AuthorBook>().HasOne(p => p.Author).WithMany(p => p.AuthorBooks).HasForeignKey(p => p.AuthorId);

            modelBuilder.Entity<Author>().HasKey(p => p.Id);
            modelBuilder.Entity<Author>().Property(p => p.FirstName).IsRequired();
            modelBuilder.Entity<Author>().Property(p => p.LastName).IsRequired();

            modelBuilder.Entity<UserBook>().HasKey(p => new { p.BookId, p.UserId });
            modelBuilder.Entity<UserBook>().HasOne(p => p.Book).WithMany(p => p.UserBooks).HasForeignKey(p => p.BookId);
            modelBuilder.Entity<UserBook>().HasOne(p => p.User).WithMany(p => p.UserBooks).HasForeignKey(p => p.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}