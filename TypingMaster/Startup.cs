using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using TypingMaster.Application;
using TypingMaster.Database;
using TypingMaster.Domain;
using TypingMaster.Hubs;

namespace TypingMaster;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Typing master API"
            });
        });

        services.AddControllers();

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
        services.AddSignalR();
        
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<TestDbContext>()
            .AddDefaultTokenProviders();

        //services.AddTransient<IInitializable, RandomTestProvider>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Typing master API"); });


        app.UseCors();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<TypingMasterHub>("/Hub");
        });

        app.UseAuthentication();

        AppInitializer.Initialize(app.ApplicationServices).Wait();
    }
}