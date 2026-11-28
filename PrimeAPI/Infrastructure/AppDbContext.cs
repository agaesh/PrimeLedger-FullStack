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
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<JournalEntryLine> JournalEntryLines { get; set; }
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

                entity.HasIndex(e => e.CodeType)
                      .HasFilter("[is_active] = 1")
                      .IsUnique()
                      .HasDatabaseName("UX_TaxRegime_Active_CodeType");
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

            modelBuilder.Entity<JournalEntryLine>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("JournalEntryLine", table =>
                {
                    table.HasCheckConstraint(
                        "CK_JournalEntryLine_OneSidedAmount",
                        "([debit] >= 0 AND [credit] >= 0) AND " +
                        "(([debit] > 0 AND [credit] = 0) OR ([credit] > 0 AND [debit] = 0))");
                });

                entity.Property(e => e.Debit)
                      .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Credit)
                      .HasColumnType("decimal(18, 2)");

                entity.HasOne(e => e.JournalEntry)
                      .WithMany(e => e.Lines)
                      .HasForeignKey(e => e.JournalEntryId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Account)
                      .WithMany()
                      .HasForeignKey(e => e.AccountId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
