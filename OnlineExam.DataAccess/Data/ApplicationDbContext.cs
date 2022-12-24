
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Models;

namespace OnlineExam.DataAccess.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>()
                .HasIndex(b => b.Subject_Name)
                .IsUnique();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
    }
}
