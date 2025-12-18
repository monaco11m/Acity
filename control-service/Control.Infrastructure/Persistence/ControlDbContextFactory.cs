using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Control.Infrastructure.Data
{
    public class ControlDbContextFactory : IDesignTimeDbContextFactory<ControlDbContext>
    {
        public ControlDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ControlDbContext>();
            optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));

            return new ControlDbContext(optionsBuilder.Options);
        }
    }
}
