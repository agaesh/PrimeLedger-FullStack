using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimeAPI.Infrasfructure;
using PrimeAPI.Domain;
using PrimeLedger.Shared.Enums;
using System.Linq;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private static readonly string TestDatabaseName = "TestDb";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Set environment to IntegrationTesting so Program.cs doesn't register SqlServer provider
            builder.UseEnvironment("IntegrationTesting");

            builder.ConfigureServices(services =>
            {
                // Remove existing DbContext registrations if any were added
                var descriptors = services.Where(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>) ||
                         d.ServiceType == typeof(AppDbContext)
                ).ToList();

                foreach (var descriptor in descriptors)
                {
                    services.Remove(descriptor);
                }

                // Register with InMemory provider using a consistent database name
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase(TestDatabaseName));

            // Build provider to resolve DbContext
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            db.Database.EnsureDeleted();   // fresh DB each run
            db.Database.EnsureCreated();

            // Seed GL accounts
            if (!db.GlAccounts.Any())
            {
                db.GlAccounts.AddRange(
                    new GlAccount
                    {
                        AccountCode = "2000",
                        AccountName = "GST Input Tax",
                        AccountType = AccountType.LIABILITY,
                        NormalBalance = NormalBalance.DEBIT,
                        AllowPosting = true,
                        IsActive = true,
                        CreatedDate = DateTime.UtcNow
                    },
                    new GlAccount
                    {
                        AccountCode = "3000",
                        AccountName = "GST Output Tax",
                        AccountType = AccountType.LIABILITY,
                        NormalBalance = NormalBalance.CREDIT,
                        AllowPosting = true,
                        IsActive = true,
                        CreatedDate = DateTime.UtcNow
                    }
                );
                db.SaveChanges();
            }
        });
    }
}
