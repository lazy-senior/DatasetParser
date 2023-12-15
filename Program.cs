using System.Text.RegularExpressions;
using Proalpha.Dataset;
using CommandLine;
namespace DatasetParser;
class Program
{

    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed<CommandLineOptions>(o => {
            if (File.Exists(o.InputPath)){
                    Dataset dataset = new();
                    dataset.Parse(o.InputPath);
                    
                    Console.WriteLine($"Informationen zu Dataset:{o.InputPath}");
                    Console.WriteLine("---");
                    dataset.PlausibilityCheck();
            } else {
                Console.WriteLine($"Dataset konnte nicht gefunden werden:\r\n\t-i:'{o.InputPath}'");
            }
        });
        

    }
}
