using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Proalpha.Dataset
{
    public class Dataset
    {

        public List<TempTable> TempTables;
        public List<string> DefinedTempTables;
        public List<DataRelation> DataRelations;
        

        private List<string> ParentTempTables;
        private List<string> ChildTempTables;
        private static readonly string DefinedTempTablePattern = @"define[^\.]*for([^\.]*)";
        //Close enough for now
        private static readonly string MultiLineCommentPattern = @"\/\*[^\*]*\*\/";

        public Dataset() {
               TempTables = new();
               DefinedTempTables = new(); 
               DataRelations = new();

               ParentTempTables = new();
               ChildTempTables = new();
        }

        public void Parse(string filePath){
            var dataSetFile = File.ReadAllText(filePath);
            ParseDataset(dataSetFile);
        }

        public void PlausibilityCheck(){
            
            Console.WriteLine($"TempTable-Includes\t:{TempTables.Count()}");
            Console.WriteLine($"TempTable-Defines\t:{DefinedTempTables.Count()}");
            Console.WriteLine($"TempTable-Relationships\t:{DataRelations.Count()}");
            Console.WriteLine("---");

            Console.WriteLine("Folgende TTs sind includiert aber nicht definiert");
            foreach(var tempTable in TempTables){
                if(!DefinedTempTables.Contains(tempTable.Name))
                    Console.WriteLine($"\t{tempTable.Name}:{tempTable.Path}");
            }

            Console.WriteLine("Folgende TTs sind definiert aber nicht includiert");
            foreach(var defTempTable in DefinedTempTables){
                if(!TempTables.Any(tt => tt.Name == defTempTable)){
                    Console.WriteLine($"\t{defTempTable}");
                }
            }

            Console.WriteLine("Folgende TTs sind definiert aber nicht in den relationships");
            foreach(var defTempTable in DefinedTempTables){
                if(!DataRelations.Any(dr => dr.ParentTempTable == defTempTable || dr.ChildTempTable == defTempTable)){
                    Console.WriteLine($"\t{defTempTable}");
                }
            }

            Console.WriteLine("Folgende TTs sind in relationships aber nicht definiert");
            foreach(var relationshipMember in DataRelations.Select(dr => dr.ParentTempTable).Concat(DataRelations.Select(dr => dr.ChildTempTable).Distinct())){
                if(!DefinedTempTables.Contains(relationshipMember)){
                    Console.WriteLine($"\t{relationshipMember}");
                }
            }
            Console.WriteLine("Folgende TTs sind in relationships aber nicht includiert");
            foreach(var relationshipMember in DataRelations.Select(dr => dr.ParentTempTable).Concat(DataRelations.Select(dr => dr.ChildTempTable).Distinct())){
                if(!TempTables.Any(tt => tt.Name == relationshipMember)){
                    Console.WriteLine($"\t{relationshipMember}");
                }
            }
            Console.WriteLine("---");
            Console.WriteLine($"Anzahl an FirstLevel-TempTables {ParentTempTables.Count()}");
            Console.WriteLine($"Anzahl an distinct Child-TempTables {ChildTempTables.Distinct().Count()}");
        }

        private void ParseDataset(string dataSetFile) {
            if(string.IsNullOrWhiteSpace(dataSetFile)) return;

            if(!TempTable.TryParseMultiple(dataSetFile, out TempTables)){
                Console.WriteLine("Fehler beim Parsen der TempTable-Includes");
            };
            if(!DataRelation.TryParseMultiple(dataSetFile, out DataRelations)){
                Console.WriteLine("Fehler beim Parsen der Relationships");
            }
            if(!TryParseDefinedTempTables(dataSetFile, out DefinedTempTables)){
                Console.WriteLine("Fehler beim Parsen der TempTable-Defines");
            }

            DataRelations.ForEach(dr => {
                ChildTempTables.Add(dr.ChildTempTable);
                if(!ChildTempTables.Contains(dr.ParentTempTable)){
                    if(!ParentTempTables.Contains(dr.ParentTempTable)){
                        ParentTempTables.Add(dr.ParentTempTable);
                    }
                }
            });   
        }

        private static bool TryParseDefinedTempTables(string defineString, out List<string> definedTempTables){
            definedTempTables = new();
            var commentRegex = new Regex(MultiLineCommentPattern, RegexOptions.IgnoreCase);
            var relationshipRegex = new Regex(DataRelation.DataRelationPattern, RegexOptions.IgnoreCase);

            defineString = commentRegex.Replace(defineString,"");
            defineString = relationshipRegex.Replace(defineString,"");

            var dttMatch = Regex.Match(defineString, DefinedTempTablePattern, RegexOptions.IgnoreCase);
            if(dttMatch.Success && dttMatch.Groups.Keys.Count() == 2)
            {
                var dttString = dttMatch.Groups[1].Value;
                definedTempTables = dttString.Split(",").Select(tt => tt.Trim()).Where(tt => tt.StartsWith("tt")).ToList<string>();
                return true;
            }
            return false;
        }
    }
}