using CommandLine;
namespace DatasetParser 
{
    internal class CommandLineOptions
    {
        [Option('i', "input", Required = true)]
        public string? InputPath { get; set; }
    }
}