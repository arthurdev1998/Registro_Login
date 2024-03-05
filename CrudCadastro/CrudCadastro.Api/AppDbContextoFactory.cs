using CrudCadastro.Data.EntityFrameWork.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CrudCadastro.Api;

public class AppDbContextoFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext  CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json")
        .Build();

        var builder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = config.GetConnectionString("CONNECTION");
        builder.UseNpgsql(connectionString, b => b.MigrationsAssembly("CrudCadastro.Api"));


        return new AppDbContext(builder.Options);
    }
}
