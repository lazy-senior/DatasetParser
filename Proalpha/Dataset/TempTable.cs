
using System.Text.RegularExpressions;

namespace Proalpha.Dataset
{
    public class TempTable 
    {
        public string Name;
        public string Path;
        private static readonly string TempTablePattern = @"{([^&}]*\.tdf)\s*{&\*}[^}]*}\s*\/\*\s*(\S*)";
        public TempTable(){}
        public TempTable(string name, string path)
        {
            this.Name = name;
            this.Path = path;
        }

        public static bool TryParse(string tempTableString, out TempTable tempTables)
        {
            tempTables = new();

            var ttMatch = Regex.Match(tempTableString, TempTablePattern, RegexOptions.IgnoreCase);
            return TryParseRegexMatchAsTempTable(ttMatch, out tempTables);
        }

        public static bool TryParseMultiple(string tempTableString, out List<TempTable> tempTables)
        {   
            tempTables = new();
            TempTable tempTable = new();
            var retValue = true;

            var ttRegex = new Regex(TempTablePattern, RegexOptions.IgnoreCase);
            var ttMatches = ttRegex.Matches(tempTableString);
            
            if(ttMatches.Count() == 0){
                return false;
            }
            foreach(var ttMatch in ttMatches.Cast<Match>()){
                if(TryParseRegexMatchAsTempTable(ttMatch, out tempTable)){
                    tempTables.Add(tempTable);
                } else {
                    Console.WriteLine($"Error[TryParseRegexMatchAsTempTable]:{ttMatch.Groups[0].Value}");
                    retValue = false;
                }
            }
            return retValue;
        }
        private static bool TryParseRegexMatchAsTempTable(Match regexMatch, out TempTable tempTable)
        {
            tempTable = new();

            if(regexMatch.Success && regexMatch.Groups.Keys.Count() == 3){
                tempTable = new()
                {
                    Path = regexMatch.Groups[1].Value,
                    Name = regexMatch.Groups[2].Value
                };
                return true;
            } 
            return false;
        }
    }
}