
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
			modelBuilder.Entity<UserSubject>()
				.HasKey(ES => new { ES.UserId, ES.SubjectId });

		}
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Admin> Admins { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<Option> Options { get; set; }
		public DbSet<UserSubject> UserSubjects { get; set; }
		public DbSet<Result> Results { get; set; }

	}
}
