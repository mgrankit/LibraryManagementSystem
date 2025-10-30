using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Issue> Issues => Set<Issue>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Username).IsRequired().HasMaxLength(100);
                b.Property(u => u.PasswordHash).IsRequired();
            });

            modelBuilder.Entity<Author>(b =>
            {
                b.HasKey(a => a.AuthorId);
                b.Property(a => a.AuthorName).IsRequired().HasMaxLength(200);
                b.HasMany(a => a.Books).WithOne(bk => bk.Author!).HasForeignKey(bk => bk.AuthorId);
            });

            modelBuilder.Entity<Book>(b =>
            {
                b.HasKey(x => x.BookId);
                b.Property(x => x.Title).IsRequired().HasMaxLength(300);
            });

            modelBuilder.Entity<Student>(b =>
            {
                b.HasKey(s => s.StudentId);
                b.Property(s => s.Name).IsRequired().HasMaxLength(200);
            });

            modelBuilder.Entity<Issue>(b =>
            {
                b.HasKey(i => i.IssueId);
                b.HasOne(i => i.Book).WithMany(bk => bk.Issues).HasForeignKey(i => i.BookId);
                b.HasOne(i => i.Student).WithMany(s => s.Issues).HasForeignKey(i => i.StudentId);
            });
        }
    }
}
