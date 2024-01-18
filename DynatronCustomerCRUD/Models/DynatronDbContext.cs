using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DynatronCustomerCRUD.Models
{
    public class DynatronDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DynatronDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Customer> Customers { get; set; }

        //seed EF migration model with previously mocked data upon create
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FirstName = "Bob", LastName = "Sclansky", Email = "bobdillon@example.com", LastUpdatedDate = DateTime.Now },
                new Customer { Id = 2, FirstName = "Elroy", LastName = "Smith", Email = "elroysmith@example.com", LastUpdatedDate = DateTime.Now },
                new Customer { Id = 3, FirstName = "Tai", LastName = "Xiaoma", Email = "taixiaoma@example.com", LastUpdatedDate = DateTime.Now },
                new Customer { Id = 4, FirstName = "Kevin", LastName = "Bacon", Email = "kevinbacon@example.com", LastUpdatedDate = DateTime.Now },
                new Customer { Id = 5, FirstName = "Alena", LastName = "Zarcovia", Email = "alenazarcovia@example.com", LastUpdatedDate = DateTime.Now }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO: refactor environment vars once secret manager is in place
            //var connectionString = Environment.GetEnvironmentVariable("DYNATRON_DB_CONNECTION_STRING");

            var connectionString = _configuration.GetConnectionString("DynatronDatabase");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
