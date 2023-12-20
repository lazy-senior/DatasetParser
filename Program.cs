
using Proalpha.Dataset;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DatasetParser.Core.Proalpa.Service;

namespace DatasetParser
{
    class Program
    {

        static void Main(string[] args)
        {

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IDatasetPlausabilityService, DatasetPlausabilityService>()
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
