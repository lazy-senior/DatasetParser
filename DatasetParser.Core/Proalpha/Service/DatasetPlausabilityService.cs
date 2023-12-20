using DatasetParser.Core.Proalpa.Database;
using DatasetParser.Core.Proalpa.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
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
            PlausibilityCheck(dataset);
        }

        private void PlausibilityCheck(Dataset dataset){
            
            var drc_Dataset = ProalphaContext.DRC_Datasets.ToList()
                .FirstOrDefault(drc_dataset => drc_dataset.DRC_Dataset_ID == dataset.DatasetName);

            var DRC_DataProviders = ProalphaContext.DRC_DataProviders
                .Where(DRC_DataProvider => DRC_DataProvider.DRC_Dataset_Obj == drc_Dataset.DRC_Dataset_Obj)
                .Join(
                    ProalphaContext.BG_Kopf,
                    drc_DataProvider => drc_DataProvider.Owning_Obj,
                    bg_Kopf => bg_Kopf.BG_Kopf_Obj,
                    (drc_DataProvider, bg_Kopf) => new {drc_DataProvider, bg_Kopf}
                );
            

    
            Console.WriteLine($"Informationen zu Dataset:{dataset.DatasetName}");
            Console.WriteLine("---");
            Console.WriteLine($"DRC_Dataset_ID\t\t:{drc_Dataset.DRC_Dataset_ID}");
            Console.WriteLine($"DatasetDefinitionFile\t:{drc_Dataset.DatasetDefinitionFile}");
            Console.WriteLine($"MasterDataValidation\t:{drc_Dataset.MasterDataValidation}");
            Console.WriteLine("---");
            foreach(var a in DRC_DataProviders){
                
                Console.WriteLine($"Formular:{a.bg_Kopf.Formular}");
            }
            Console.WriteLine("---");
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