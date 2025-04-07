using Microsoft.EntityFrameworkCore;

namespace project2233
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.UseSqlite("Data Source=library.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Article)
                    .IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description);

                entity.Property(e => e.Status)
                    .HasMaxLength(50);

                entity.Property(e => e.Reader)
                    .HasMaxLength(100);
            });
        }
    }
}