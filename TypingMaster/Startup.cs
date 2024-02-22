using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using TypingMaster.Application;
using TypingMaster.Application.Hubs;
using TypingMaster.Controllers;
using TypingMaster.Database;
using TypingMaster.Domain;
using TypingMaster.Middlewares;

namespace TypingMaster;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IConfiguration>(_ => new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", false, true)
            .Build());
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Typing master API"
            });
        });


        services.AddControllers(options =>
        {
            var backendSettings = Configuration.GetSection(EndpointsSettings.SectionName).Get<EndpointsSettings>();
            var apiEndpoint = backendSettings?.ApiEndpoint;
            if(!string.IsNullOrWhiteSpace(apiEndpoint))
                options.Conventions.Add(new RoutePrefixConvention(apiEndpoint));
        });
        
        services.AddCors(x => x.AddDefaultPolicy(new CorsPolicyBuilder()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true)
            .Build()));

        services.AddMediatR(cfg =>
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a =>
                {
                    var name = a.GetName().Name;
                    return name != null && name.StartsWith("TypingMaster.");
                });
            cfg.RegisterServicesFromAssemblies(assemblies.ToArray());
        });

        services.AddApplication();
        services.AddCore();
        services.AddDatabase();

        services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<TestDbContext>()
            .AddDefaultTokenProviders();
        
        services.AddOptions<EndpointsSettings>()
            .Configure<IConfiguration>((o, c) => c.GetSection(EndpointsSettings.SectionName).Bind(o));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<EndpointsSettings> endpointSettings)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Typing master API"); });
        
        app.UseCors();

        app.UseMiddleware<RequestTimingMiddleware>();
        app.UseEndpoints(endpoints =>
        {
            
            endpoints.MapControllers();
            endpoints.MapHub<TypingMasterHub>($"/{endpointSettings.Value.HubEndpoint}");
        });

        app.UseAuthentication();
        
    }
}