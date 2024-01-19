using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DynatronCustomerCRUD.Models
{
    public class DynatronDbContext : DbContext
    {
        //private readonly IConfiguration _configuration;

        //public DynatronDbContext(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public DynatronDbContext(DbContextOptions<DynatronDbContext> options)
            : base(options)
        {
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
            if (!optionsBuilder.IsConfigured)
            {
                // Configure the context to use an SQL server (or any other provider)
                // only if it hasn't been configured already. This is useful for unit tests.
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DynatronDatabase");
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
    }
}
