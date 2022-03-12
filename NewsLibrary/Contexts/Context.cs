using Microsoft.EntityFrameworkCore;
using NewsLibrary.Models;

namespace NewsLibrary.Contexts
{
    public class Context : DbContext
    {
        private readonly string _connectionString;

        public DbSet<NewsContain> NewsContains { get; set; }

        public Context(string connection)
        {
            _connectionString = connection;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(_connectionString);
        }
    }
}
