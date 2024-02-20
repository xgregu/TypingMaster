using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using NLog.Extensions.Logging;
using TypingMaster.UI.Components.PleaseWait;
using TypingMaster.UI.Localizations;

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

        services.AddControllers()
            .AddControllersAsServices();

        services.AddRazorPages();

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

        services.AddScoped<IPleaseWaitService, PleaseWaitService>();

        services.AddTypingMasterLocalizations();
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
            endpoints.MapControllers();
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });

        var supportedCultures = CultureConstants.SupportedCultures.Select(x => x.Name).ToArray();

        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(CultureConstants.DefaultCulture.Name)
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);
    }
}