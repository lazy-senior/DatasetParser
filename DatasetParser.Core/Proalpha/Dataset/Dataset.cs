using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Proalpha.Dataset
{
    public class Dataset
    {

        public List<TempTable> TempTables;
        public List<string> DefinedTempTables;
        public List<DataRelation> DataRelations;
        
        private static readonly string DefinedTempTablePattern = @"define[^\.]*for([^\.]*)";
        //Close enough for now
        private static readonly string MultiLineCommentPattern = @"\/\*[^\*]*\*\/";

        public Dataset() {
               TempTables = new();
               DefinedTempTables = new(); 
               DataRelations = new();
        }

        public void Parse(string filePath){
            var dataSetFile = File.ReadAllText(filePath);
            ParseDataset(dataSetFile);
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