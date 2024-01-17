using CSVHandler.UI.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVHandler.UI.Data
{
    public class ApplicationContext : DbContext
    {
        private const string CONNECTION_STRING = "Server=.\\MKMSSQLSERVER;Database=PeopleDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public DbSet<Person> People { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
        }
    }
}
