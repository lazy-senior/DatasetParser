
using Proalpha.Dataset;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DatasetParser.Core.Proalpa.Service;
using Microsoft.Extensions.Configuration;
using DatasetParser.Core.Proalpa.Database;
using Microsoft.EntityFrameworkCore;

namespace DatasetParser
{
    class Program
    {

        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            IConfiguration configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddDbContext<ProalphaContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("Proalpha_Test"))
                )
                .AddSingleton<IDatasetPlausabilityService, DatasetPlausabilityService>()
                .AddScoped<IConfiguration>(_ => configuration)
                .BuildServiceProvider();

            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed<CommandLineOptions>(o => {
                if (File.Exists(o.InputPath)){
                       var datasetPlausabilityService = serviceProvider.GetService<IDatasetPlausabilityService>();
                       datasetPlausabilityService.PrintCheck(o.InputPath);
                } else {
                    Console.WriteLine($"Dataset konnte nicht gefunden werden:\r\n\t-i:'{o.InputPath}'");
                }
            });

        }
    }
}
