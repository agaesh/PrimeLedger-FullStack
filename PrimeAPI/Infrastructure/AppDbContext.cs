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

        public DbSet<TaxTreatment> TaxTreatments { get; set; }
        public DbSet<TaxRegime> TaxRegime { get; set; }
        public DbSet<GlAccount> GlAccounts { get; set; }
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

            modelBuilder.Entity<TaxTreatment>(entity =>
            {
                // Unique index to prevent exact duplicates
                entity.HasIndex(e => new { e.Code})
                      .IsUnique();

                entity.Property(e => e.Type)
                      .HasConversion<string>()
                      .HasMaxLength(10);

                // Foreign key relationship to GlAccount
                entity.HasOne(e => e.PurchaseGL)
                      .WithMany()
                      .HasForeignKey(e => e.PurchaseGLId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SalesGL)
                      .WithMany()
                      .HasForeignKey(e => e.SalesGLId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Only one active regime per CodeType
                entity.HasIndex(e => e.Code)
                      .HasFilter("[is_active] = 1")
                      .IsUnique()
                      .HasDatabaseName("UQ_TaxRegime_Active");

                // Add check constraint via ToTable
                entity.ToTable("TaxTreatment", tb =>
                {
                    tb.HasCheckConstraint("CK_TaxTreatment_DifferentAccounts",
                        "[purchase_gl_id] <> [sales_gl_id]");
                });
                });

            modelBuilder.Entity<TaxRegime>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CodeType)
                      .HasConversion<string>()
                      .HasMaxLength(10);
            });

            modelBuilder.Entity<GlAccount>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.AccountType)
                      .HasConversion<string>()
                      .HasMaxLength(50);
                entity.Property(e => e.NormalBalance)
                      .HasConversion<string>()
                      .HasMaxLength(6);

                entity.HasOne(e => e.ParentAccount)
                      .WithMany(e => e.ChildAccounts)
                      .HasForeignKey(e => e.ParentAccountId)
                      .OnDelete(DeleteBehavior.Restrict); // prevent cascade delete
            });
        }
    }
}
