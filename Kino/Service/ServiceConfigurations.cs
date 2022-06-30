using Kino.Common.IService;

namespace Kino.Service;

public static class ServiceConfigurations
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
    }
}