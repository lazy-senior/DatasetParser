using DatasetParser.Core.Proalpa.Database;
using Proalpha.Dataset;

namespace DatasetParser.Core.Proalpa.Service
{
    class DatasetPlausabilityService : IDatasetPlausabilityService
    {

        public ProalphaContext ProalphaContext { get; }
        
        public DatasetPlausabilityService(ProalphaContext proalphaContext)
        {
            ProalphaContext = proalphaContext;
        }

        public void PrintCheck(string filePath)
        {

            Dataset dataset = new();
            dataset.Parse(filePath);
            
            Console.WriteLine($"Informationen zu Dataset:{filePath}");
            Console.WriteLine("---");
            PlausibilityCheck(dataset);
        }

        private void PlausibilityCheck(Dataset dataset){
            
            Console.WriteLine($"TempTable-Includes\t:{dataset.TempTables.Count()}");
            Console.WriteLine($"TempTable-Defines\t:{dataset.DefinedTempTables.Count()}");
            Console.WriteLine($"TempTable-Relationships\t:{dataset.DataRelations.Count()}");
            Console.WriteLine("---");

            Console.WriteLine("Folgende TTs sind includiert aber nicht definiert");
            foreach(var tempTable in dataset.TempTables){
                if(!dataset.DefinedTempTables.Contains(tempTable.Name))
                    Console.WriteLine($"\t{tempTable.Name}:{tempTable.Path}");
            }

            Console.WriteLine("Folgende TTs sind definiert aber nicht includiert");
            foreach(var defTempTable in dataset.DefinedTempTables){
                if(!dataset.TempTables.Any(tt => tt.Name == defTempTable)){
                    Console.WriteLine($"\t{defTempTable}");
                }
            }

            Console.WriteLine("Folgende TTs sind definiert aber nicht in den relationships");
            foreach(var defTempTable in dataset.DefinedTempTables){
                if(!dataset.DataRelations.Any(dr => dr.ParentTempTable == defTempTable || dr.ChildTempTable == defTempTable)){
                    Console.WriteLine($"\t{defTempTable}");
                }
            }

            Console.WriteLine("Folgende TTs sind in relationships aber nicht definiert");
            foreach(var relationshipMember in dataset.DataRelations.Select(dr => dr.ParentTempTable).Concat(dataset.DataRelations.Select(dr => dr.ChildTempTable).Distinct())){
                if(!dataset.DefinedTempTables.Contains(relationshipMember)){
                    Console.WriteLine($"\t{relationshipMember}");
                }
            }
            Console.WriteLine("Folgende TTs sind in relationships aber nicht includiert");
            foreach(var relationshipMember in dataset.DataRelations.Select(dr => dr.ParentTempTable).Concat(dataset.DataRelations.Select(dr => dr.ChildTempTable).Distinct())){
                if(!dataset.TempTables.Any(tt => tt.Name == relationshipMember)){
                    Console.WriteLine($"\t{relationshipMember}");
                }
            }


            List<string> parentTempTables = new();
            List<string> childTempTables = new();

             dataset.DataRelations.ForEach(dr => {
                childTempTables.Add(dr.ChildTempTable);
                if(!childTempTables.Contains(dr.ParentTempTable)){
                    if(!parentTempTables.Contains(dr.ParentTempTable)){
                        parentTempTables.Add(dr.ParentTempTable);
                    }
                }
            });   

            Console.WriteLine("---");
            Console.WriteLine($"Anzahl an FirstLevel-TempTables {parentTempTables.Count()}");
            Console.WriteLine($"Anzahl an distinct Child-TempTables {childTempTables.Distinct().Count()}");
        }
    }
}