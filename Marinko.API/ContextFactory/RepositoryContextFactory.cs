using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Marinko.API.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                          .AddJsonFile("appsettings.json")
                                                          .Build();

            var builder = new DbContextOptionsBuilder<RepositoryContext>().UseSqlServer(configuration.GetConnectionString("Default"));
            return new RepositoryContext(builder.Options);
        }
    }
}
