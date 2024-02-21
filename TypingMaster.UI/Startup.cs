using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Easy.MessageHub;
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

        services.AddOptions<BackendSettings>()
            .Configure<IConfiguration>((o, c) => c.GetSection(BackendSettings.SectionName).Bind(o));
        

        services.AddHttpClient<ApiClient>("ApiClient", client =>
        {
            var backendSettings = Configuration.GetSection(BackendSettings.SectionName).Get<BackendSettings>();
            if (backendSettings?.Gateway != null)
                client.BaseAddress = new Uri(backendSettings.Gateway + "/api/");
        });

        services.AddTransient<ApiClient>();
        services.AddMemoryCache();

        services.AddScoped<IPleaseWaitService, PleaseWaitService>();

        services.AddTypingMasterLocalizations();
        
        services.AddSignalR();
        services.AddSingleton<SignalRConnectivity>();
        services.AddHostedService<SignalRConnectivity>(provider => provider.GetRequiredService<SignalRConnectivity>());
        services.AddSingleton<IMessageHub, MessageHub>();
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

        app.ConfigureLocalizations();
    }
}