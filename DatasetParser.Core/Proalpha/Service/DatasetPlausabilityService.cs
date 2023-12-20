using Proalpha.Dataset;

namespace DatasetParser.Core.Proalpa.Service
{
    class DatasetPlausabilityService : IDatasetPlausabilityService
    {
        public void PrintCheck(string filePath)
        {
            Dataset dataset = new();
            dataset.Parse(filePath);
            
            Console.WriteLine($"Informationen zu Dataset:{filePath}");
            Console.WriteLine("---");
            dataset.PlausibilityCheck();
        }
    }
}