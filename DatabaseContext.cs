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
                optionsBuilder.UseSqlServer(
                    "Server=dbsrv\\dub2024;Database=oshkinng207b2;Integrated Security=True;TrustServerCertificate=True");
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

                entity.Property(e => e.Description)
                    .HasColumnType("nvarchar(max)");

                entity.Property(e => e.Status)
                    .HasMaxLength(50);

                entity.Property(e => e.Reader)
                    .HasMaxLength(100);

                entity.Property(e => e.IssueDate)
                    .HasColumnType("datetime");

                entity.Property(e => e.ReturnDate)
                    .HasColumnType("datetime");
            });
        }
    }
}