using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace QuidditchTrip.API.Configuration.Database;

public class QuidditchContextDesignTimeFactory : IDesignTimeDbContextFactory<QuidditchContext>
{
    public QuidditchContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Environment variable 'CONNECTION_STRING' is not set.");

        var optionsBuilder = new DbContextOptionsBuilder<QuidditchContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new QuidditchContext(optionsBuilder.Options);
    }

}
