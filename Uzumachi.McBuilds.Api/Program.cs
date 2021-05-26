using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Uzumachi.McBuilds.Migrations;

namespace Uzumachi.McBuilds.Api {
  public class Program {

    public static void Main(string[] args) {
      var webHost = CreateHostBuilder(args).Build();

      using( var scope = webHost.Services.CreateScope() ) {
        DatabaseInitializer.Migrations(scope.ServiceProvider);
      }

      webHost.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => {
          webBuilder.UseStartup<Startup>();
        });
  }
}
