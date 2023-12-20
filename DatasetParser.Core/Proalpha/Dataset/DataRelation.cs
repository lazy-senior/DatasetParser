using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Proalpha.Dataset
{
    public class DataRelation: IEqualityComparer<DataRelation>
    {
        public string Name;
        public bool Inverse; 
        public bool Nested;
        public string ParentTempTable;
        public string ChildTempTable;
        public Dictionary<string, string> Fields;

        /*
            1.	i
            2.	DocTypeBOrderDocHead
            3.	ttX
            4.	ttY
            5.	A ,A,
                B ,B,
                C ,D
            6.	nested
        */
        public static readonly string DataRelationPattern = @"data-relation\s*dr(i?)_?(\S*)\s*for\s*([^,]*),\s*(\S*)[^r]*relation-fields[^\(]*\(([^)]*)\)[\s]*(nested)?";

        public DataRelation()
        {
            Fields = new();
        }

        
        public static bool TryParse(string dataRelationString, out DataRelation dataRelation)
        {
            dataRelation = new();

            var relMatch = Regex.Match(dataRelationString, DataRelationPattern, RegexOptions.IgnoreCase);
            return TryParseRegexMatchAsDataRelation(relMatch, out dataRelation);
        }

        public static bool TryParseMultiple(string dataRelationString, out List<DataRelation> dataRelations)
        {
            dataRelations = new();
            DataRelation dataRelation = new();
            bool retValue = true;

            var relRegex = new Regex(DataRelationPattern, RegexOptions.IgnoreCase);
            var relMatches = relRegex.Matches(dataRelationString);

            if(relMatches.Count() == 0){
                return false;
            }
            foreach(var relMatch in relMatches.Cast<Match>()){
                if(TryParseRegexMatchAsDataRelation(relMatch, out dataRelation)){
                    dataRelations.Add(dataRelation);
                } else {
                    retValue = false;
                }
            }

            return retValue;
        }

        private static bool TryParseRegexMatchAsDataRelation(Match regexMatch, out DataRelation dataRelation){
            
            dataRelation = new();
            
            if(regexMatch.Success && regexMatch.Groups.Keys.Count() == 7)
            {
                dataRelation = new(){
                    Inverse            = regexMatch.Groups[1].Value.ToLower() == "i",
                    Name               = regexMatch.Groups[2].Value,
                    ParentTempTable    = regexMatch.Groups[3].Value,
                    ChildTempTable     = regexMatch.Groups[4].Value,
                    Nested             = regexMatch.Groups[6].Value.ToLower() == "nested"
                };

                var relSplit = regexMatch.Groups[5].Value.Split(",");

                for(var i = 0; i < relSplit.Count(); i+=2) 
                {
                    dataRelation.Fields.Add(relSplit[i],relSplit[i+1]);
                }

                return true;
            }
            return false;
        }

        public override bool Equals(object? obj)
        {
            if(obj is not DataRelation)
                return false;
            return Equals((DataRelation)obj);
        }

        public bool Equals(DataRelation other){
            return Equals(this,other);
        }

        public bool Equals(DataRelation? x, DataRelation? y)
        {
            if(x == null || y == null)
                return false;
                
            return x.ParentTempTable == y.ParentTempTable && x.ChildTempTable == y.ChildTempTable;
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        public int GetHashCode([DisallowNull] DataRelation obj)
        {
            return obj.ParentTempTable.GetHashCode() ^ this.ChildTempTable.GetHashCode();
        }
    }
}