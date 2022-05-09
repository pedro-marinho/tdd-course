using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CursoOnline.Dominio.Data.Context
{
    public class CourseDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Course>().Ignore(c => c.Validator);
            builder.Entity<Course>().Property(c => c.Name).IsRequired();
            builder.Entity<Course>().Property(c => c.Description).IsRequired(false);

            builder.Entity<Student>().Ignore(s => s.Validator);
            builder.Entity<Student>().Property(s => s.Name).IsRequired();
            builder.Entity<Student>().Property(s => s.Email).IsRequired();
            builder.Entity<Student>().Property(s => s.Cpf).IsRequired();

            builder.Entity<Enrollment>().Ignore(e => e.Validator);
            builder.Entity<Enrollment>().Property(e => e.PricePaid).IsRequired();
            builder.Entity<Enrollment>().Property(e => e.Discounted).IsRequired();
        }
    }
}
