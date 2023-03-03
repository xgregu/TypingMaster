using System.Reflection;
using System.Text.Json.Serialization;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using MediatR;
using MediatR.Courier.DependencyInjection;
using NLog.Extensions.Logging;
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
        var isPortable = Environment.GetCommandLineArgs().Contains("-portable");

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

        services.AddDatabase(isPortable);

        services.AddTransient<ITestService, TestService>();

        var files = Directory
            .EnumerateFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                $"{Constants.AppName}.*.dll",
                SearchOption.AllDirectories)
            .ToList();
        var assemblies = files.Select(Assembly.LoadFrom).ToList();
        assemblies.Add(Assembly.GetExecutingAssembly());
        services.AddMediatR(assemblies.ToArray());
        services.AddCourier(assemblies.ToArray());
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