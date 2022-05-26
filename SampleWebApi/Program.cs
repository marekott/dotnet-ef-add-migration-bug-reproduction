using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace SampleWebApi
{
    public class Program
    {
        public static Task Main(string[] args) => WebHostHelper.BuildAndRunWebHostAsync<Startup>(args, validateDiConfigurationInDebug: true);

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }

    public static class WebHostHelper
    {
        public static async Task BuildAndRunWebHostAsync<TStartup>(string[] args, bool validateDiConfigurationInDebug = false)
            where TStartup : class
        {
            await RunHostAsync<TStartup>(args, validateDiConfigurationInDebug);
        }

        internal static async Task RunHostAsync<TStartup>(string[] args, bool validateDiConfigurationInDebug = false) where TStartup : class
        {
            var webHost = BuildWebHost<TStartup>(validateDiConfigurationInDebug);

            await webHost.RunAsync();
        }

        private static IWebHost BuildWebHost<TStartup>(bool validateDiConfigurationInDebug = false) where TStartup : class =>
            CreatePreconfiguredWebHostBuilder<TStartup>(validateDiConfigurationInDebug).Build();

        internal static IWebHostBuilder CreatePreconfiguredWebHostBuilder<TStartup>(bool validateDiConfigurationInDebug = false) where TStartup : class
        {
            var hostBuilder = new WebHostBuilder()
                .ConfigureKestrel((context, options) =>
                {
                    options.AllowSynchronousIO = false;
                })
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) => ConfigureConfigurationSources(hostingContext, config))
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = validateDiConfigurationInDebug;
                })
                .UseStartup<TStartup>();

            return hostBuilder;
        }

        private static void ConfigureConfigurationSources(WebHostBuilderContext hostingContext, IConfigurationBuilder config)
        {
            var env = hostingContext.HostingEnvironment.EnvironmentName;

            config
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: false)
                .AddJsonFile("appsettings.hosting.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
        }
    }
}
