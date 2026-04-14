using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SchoolModel;

namespace SchoolWpfApp.Data
{
    public class SchoolContextFactory : IDesignTimeDbContextFactory<SchoolContext>
    {
        public SchoolContext CreateDbContext(string[]? args = null)
        {
            // Load configuration from appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            var connString = config.GetConnectionString("SchoolDb");

            var options = new DbContextOptionsBuilder<SchoolContext>()
                 .UseSqlServer(connString)
                 .EnableSensitiveDataLogging()
                 .LogTo(Console.WriteLine)              //--< Log SQL statements to the Console <<<
                 .Options;

            return new SchoolContext(options);
        }
    } 
}