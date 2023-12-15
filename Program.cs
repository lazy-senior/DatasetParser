using System.Text.RegularExpressions;
using Proalpha.Dataset;

namespace DatasetParser;
class Program
{

    static void Main(string[] args)
    {
        Dataset dataset = new ();
        dataset.Parse("C:\\Users\\boening\\Downloads\\dataset.pds");

        Console.WriteLine($"TempTable-Includes\t:{dataset.TempTables.Count()}");
        Console.WriteLine($"TempTable-Defines\t:{dataset.DefinedTempTables.Count()}");
        Console.WriteLine($"TempTable-Relationships\t:{dataset.DataRelations.Count()}");

        dataset.PlausibilityCheck();

    }
}
