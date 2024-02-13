using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using NLog.Extensions.Logging;

namespace TypingMaster.UI;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddBlazorise()
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();
        
        services.AddRazorComponents()
            .AddInteractiveServerComponents();
        
        services.AddLogging(x => { x.AddNLog(); });

        services.AddSingleton<IConfiguration>(_ => new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", false, true)
            .Build());

        
        services.AddHttpClient<ApiClient>("ApiClient", client =>
        {
            var backendSettings = Configuration.GetSection(BackendSettings.SectionName).Get<BackendSettings>();
            if (backendSettings?.ApiGateway != null) 
                client.BaseAddress = new Uri(backendSettings.ApiGateway);
        });
        services.AddTransient<ApiClient>();
        
        services.AddMemoryCache();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
        IHostApplicationLifetime applicationLifetime)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseStaticFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}