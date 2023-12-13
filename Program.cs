using System.Text.RegularExpressions;

namespace DatasetParser;
class Program
{
    public static Dictionary<string,string> includedTempTables = new();
    public static List<string> definedTempTables = new();
    public static List<string> firstLevelTables = new();
    public static List<string> secondPlusLevelTables = new();
    
    static void Main(string[] args)
    {
        var linesRead = File.ReadLines("C:\\Users\\boening\\Documents\\Workspace\\DatasetParser\\dataset.pds");
        readTempTables(linesRead);
        readUsedTempTables(linesRead);
        
        Console.WriteLine("Folgende TTs sind includiert aber nicht definiert");
        foreach(var tempTable in includedTempTables){
            if(!definedTempTables.Contains(tempTable.Value))
            Console.WriteLine($"\t{tempTable.Value}:{tempTable.Key}");
        }

        Console.WriteLine("Folgende TTs sind definiert aber nicht includiert");
        foreach(var tempTable in definedTempTables){
            if(!includedTempTables.ContainsValue(tempTable))
            Console.WriteLine($"\t{tempTable}");
        }

        Console.WriteLine("Folgende TTs sind definiert aber nicht in den relationships");
        foreach(var tempTable in definedTempTables){
            if(!secondPlusLevelTables.Contains(tempTable) && !firstLevelTables.Contains(tempTable)){
                Console.WriteLine($"\t{tempTable}");
            }
        }

        Console.WriteLine($"Inkludierte Temp-Tables:{includedTempTables.Count()}, davon im Define-Statement:{definedTempTables.Count()}");
        Console.WriteLine($"Top-Level Temp-Tables:{firstLevelTables.Count()}, untergeordnete Temp-Tables:{secondPlusLevelTables.Count()}");
    }


    static void readUsedTempTables(IEnumerable<string>? lines){
        if(lines is null) return;

        string currLine;
        var inDefineStatement = false;

        for(var i=0;i < lines.Count();i++)
        {
            currLine = lines.ElementAt(i).Trim(); 
            if(currLine.StartsWith("define")){
                inDefineStatement = true;
            }

            if(currLine.StartsWith("data-relation")){
                inDefineStatement = false;
                var regexRelationship = Regex.Match(currLine, @"\S*\s*,\s*\S*", RegexOptions.IgnoreCase);
                if(regexRelationship.Success){
                    var arr = regexRelationship.Value.Split(",");
                    (string first, string second) = (arr[0].Trim(), arr[1].Trim());
                    secondPlusLevelTables.Add(second);
                    if(!secondPlusLevelTables.Contains(first)){
                        if(!firstLevelTables.Contains(first)){
                            firstLevelTables.Add(first);
                        }
                    }
                }
            }

            if(inDefineStatement){
                if(currLine.StartsWith("tt")){
                    var tt = currLine.Replace(" ","").Replace(",","");
                    if(!definedTempTables.Contains(tt)){
                        definedTempTables.Add(tt);
                    } else {
                        Console.WriteLine($"{tt} doppelt definiert.");
                    }
                }
            }

        }
    }

    static void readTempTables(IEnumerable<string>? lines){
        
        if(lines is null) return;

        foreach(var lineRead in lines){
            
            if(lineRead.Trim().StartsWith("/*") || lineRead.Trim().StartsWith("//"))
                continue;

            if(lineRead.Trim().StartsWith("{")){
                var includeFile = String.Empty;
                var includeComment = String.Empty;
                var regexInclude = Regex.Match(lineRead.Trim().Replace("{&*}",""), @"{[^&}]*\.tdf[^}]*}", RegexOptions.IgnoreCase);
                if(regexInclude.Success){
                    includeFile = regexInclude.Value;
                    var regexComment = Regex.Match(lineRead, @"/\*\s*\S*", RegexOptions.IgnoreCase);
                    if(regexComment.Success){
                        includeComment = regexComment.Value.Replace("/*","").Trim(); 
                    }
                }

                if(includeFile != String.Empty){
                    if(!includedTempTables.ContainsKey(includeFile)){
                        includedTempTables.Add(includeFile, includeComment);
                    } else {
                        Console.WriteLine($"{includeFile} wird doppelt eingebunden");
                    }
                }
            }
        }
    }
}
