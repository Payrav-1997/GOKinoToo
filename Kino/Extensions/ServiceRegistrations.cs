using Kino.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Kino.Extensions;

public static class ServiceRegistrations
{
    public static void ConfigureDataContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<DataContext>(builder => builder.UseNpgsql(configuration.GetConnectionString("DB_CONNECTION_STRING")).UseLazyLoadingProxies());

    public static void ConfigureRouting(this IServiceCollection services) =>
        services.AddRouting(x=>x.LowercaseUrls = true);
    
    public static void ConfigureStaticFiles(this IApplicationBuilder app)
    {
        
       // var rootDirectory = Environment.GetEnvironmentVariable("WebRootPath");
            var rootDirectory = Path.GetFullPath("wwwroot/images/");
        if (!Directory.Exists(rootDirectory))
            Directory.CreateDirectory(rootDirectory);

        app.UseStaticFiles(new StaticFileOptions {
            FileProvider =  new PhysicalFileProvider(rootDirectory),
            RequestPath = new PathString("/storage")
        });
    }


  
}