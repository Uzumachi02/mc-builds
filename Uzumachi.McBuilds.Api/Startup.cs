using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Npgsql;
using System.Data;
using Uzumachi.McBuilds.Core.Services;
using Uzumachi.McBuilds.Core.Services.Interfaces;
using Uzumachi.McBuilds.Data;
using Uzumachi.McBuilds.Data.Interfaces;

namespace Uzumachi.McBuilds.Api {

  public class Startup {

    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) {
      _configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {

      services.AddControllers();
      services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Uzumachi.McBuilds.Api", Version = "v1" });
      });

      Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
      var connectionString = _configuration.GetConnectionString("DefaultConnection");

      // dependency injection
      services.AddTransient<IDbConnection>(_ => new NpgsqlConnection(connectionString));
      services.AddScoped<IUnitOfWork, UnitOfWork>();

      // injection services
      services.AddScoped<IPostsService, PostsService>();
      services.AddScoped<ILikesService, LikesService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if( env.IsDevelopment() ) {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Uzumachi.McBuilds.Api v1"));
      }

      //app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
    }
  }
}
