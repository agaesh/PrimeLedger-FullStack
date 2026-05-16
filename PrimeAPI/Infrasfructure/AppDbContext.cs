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
    }
}
