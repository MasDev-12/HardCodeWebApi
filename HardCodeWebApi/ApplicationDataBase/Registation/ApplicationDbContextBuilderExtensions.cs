using Microsoft.EntityFrameworkCore;

namespace HardCodeWebApi.ApplicationDataBase.Registation;

public static class ApplicationDbContextBuilderExtensions
{
    public static WebApplicationBuilder AddDataBase(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>
            (options =>
            options.UseNpgsql(webApplicationBuilder.Configuration.GetConnectionString("ApplicationDbContext"))
            , contextLifetime: ServiceLifetime.Scoped
            , optionsLifetime: ServiceLifetime.Singleton);

        return webApplicationBuilder;
    }
}
