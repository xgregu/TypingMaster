using System.Text.Json.Serialization;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using MediatR;
using MediatR.Courier.DependencyInjection;
using NLog.Extensions.Logging;
using TypingMaster.Browser;
using TypingMaster.Browser.Hubs;
using TypingMaster.Database;
using TypingMaster.Domain;
using TypingMaster.Domain.Options;

namespace TypingMaster;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var serverMode = Convert.ToBoolean(Configuration.GetRequiredSection("ServerMode").Value);

        services.AddBlazorise()
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();

        services.AddControllers()
            .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        services.AddRazorPages();
        services.AddServerSideBlazor();

        services.AddLogging(x => { x.AddNLog(); });

        services.AddSingleton<IConfiguration>(_ => new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", true, true)
            .Build());

        services.AddSignalR();

        services.AddOptions<TypingTestOptions>().Bind(Configuration.GetSection(TypingTestOptions.SectionKey));

        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
        services.AddCourier(AppDomain.CurrentDomain.GetAssemblies());

        services.AddDatabase(serverMode);
        services.AddBrowser(serverMode);
        services.AddDomain();
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
            endpoints.MapHub<BrowserHub>("/BrowserHub");
        });
    }
}