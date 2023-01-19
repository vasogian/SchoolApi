using Microsoft.EntityFrameworkCore;

namespace SchoolApi.Models
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options)
           : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<Professor> Professors { get; set; }
        
    }
}
