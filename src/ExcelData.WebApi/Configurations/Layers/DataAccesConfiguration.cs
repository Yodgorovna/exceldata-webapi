using ExcelData.DataAccess.Interfaces.Users;
using ExcelData.DataAccess.Repositories.Users;

namespace ExcelData.WebApi.Configurations.Layers;

public static class DataAccesConfiguration
{
    public static void ConfigureDataAcces(this WebApplicationBuilder builder)
    {
        //--> DI containers, IoC containers
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }
}
