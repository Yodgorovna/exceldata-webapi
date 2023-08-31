using ExcelData.Service.Interfaces.Users;
using ExcelData.Service.Services.Users;

namespace ExcelData.WebApi.Configurations.Layers
{
    public static class ServiceLayerConfiguration
    {
        public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
        {
            //--> DI containers, IoC containers
            builder.Services.AddScoped<IUserService, UserService>();
        }
    }
}
