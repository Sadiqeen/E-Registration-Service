using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Models.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<TableUser> TableUser { get; set; }

        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TableUser>(entity =>
            {
                entity.ToTable("TableUser");
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("getutcdate()");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("getutcdate()");
                entity.HasIndex(u => u.Email)
                    .IsUnique();
            });
        }
    }
}
