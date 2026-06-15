namespace PrimeAPI.Infrasfructure
{
    using Microsoft.EntityFrameworkCore;
    using PrimeAPI.Domain;
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProductMetadata> ProductMetadata { get; set; }

        public DbSet<TaxCodeSetup> TaxCodeSetups { get; set; }
        public DbSet<TaxCodeHistory> TaxCodeHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductMetadata>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Tell EF Core to store the Enum as a string/text in the DB
                entity.Property(e => e.Status)
                      .HasConversion<string>()
                      .HasMaxLength(50); // Good practice to limit the text column size

                entity.HasOne(p => p.Parent)              // navigation to parent
                     .WithMany(p => p.Children)          // navigation to children
                     .HasForeignKey(p => p.ParentId)     // FK column
                     .OnDelete(DeleteBehavior.Restrict); // prevent cascade delete

                entity.Property(e=> e.type)
                      .HasConversion<string>()
                      .HasMaxLength(50);

                // Tell the database to enforce uniqueness on the Code column
                modelBuilder.Entity<ProductMetadata>()
                    .HasIndex(p => p.Code)
                    .IsUnique();
            });

            modelBuilder.Entity<TaxCodeSetup>(entity =>
            {
                // Unique index to prevent exact duplicates
                entity.HasIndex(e => new { e.Code})
                      .IsUnique();

                entity.Property(e => e.Type)
                      .HasConversion<string>()
                      .HasMaxLength(10);
            });

            modelBuilder.Entity<TaxCodeHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
                // Tell EF Core to store the Enum as a string/text in the DB
            });
        }
    }
}
