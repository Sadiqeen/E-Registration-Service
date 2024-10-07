using Microsoft.EntityFrameworkCore;

namespace CourseService.Models.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<TableCourse> TableCourse { get; set; }

        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TableCourse>(entity =>
            {
                entity.ToTable("TableCourse");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("getutcdate()");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("getutcdate()");
            });
        }
    }
}
