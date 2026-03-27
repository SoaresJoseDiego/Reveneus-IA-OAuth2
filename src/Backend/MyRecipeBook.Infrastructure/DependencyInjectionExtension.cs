using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        AddDbContext_MySqlServer(services);
        AddRepositories(services);
    }

    private static void AddDbContext_MySqlServer(IServiceCollection services)
    {
        var connectionString = "Server=127.0.0.1;Port=3306;Database=MyRecipeBook;Uid=root;Pwd=;";
        var serverVersion = new MySqlServerVersion(new Version(10,4,32));

        services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseMySql(connectionString, serverVersion);
        });
    }


    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUser, UserRepository>();
    }

}
