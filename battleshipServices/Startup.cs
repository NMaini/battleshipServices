using System.Reflection;
using battleshipServices.Model;
using battleshipServices.Services;
using battleshipServices.Middleware;
using battleshipServices.Filters;
using Microsoft.OpenApi.Models;

namespace battleshipServices;

/// <summary>
/// Start-Up Class.
/// </summary>
public class Startup
{
    /// <summary>
    /// Contructor
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// Configuration.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container
    /// </summary>
    /// <param name="services">Container</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen(
            config => { 
                config.OperationFilter<OperationFilter>();
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Battleship API",
                    Description = "An ASP.NET Core Web API for supporting Battleship state tracking.",
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

        services.AddHttpContextAccessor();
        services.AddSingleton<BattleshipSingleton>();
        services.AddScoped<IBoardService, BoardService>();
        services.AddScoped<IShipService, ShipService>();
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    /// </summary>
    /// <param name="app">AppBuilder</param>
    /// <param name="env">Local Env</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(); 
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseValidationMiddleware();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}