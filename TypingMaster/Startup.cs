using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using NLog.Extensions.Logging;
using System.Text.Json.Serialization;
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

        services.AddOptions<TypingTestOptions>().Bind(Configuration.GetSection(TypingTestOptions.SectionKey));

        services.AddDatabase();


        services.AddTransient<ITestService, TestService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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