using System.Collections.Generic;
using System.IO;
using AnalyzeFile.Interface;
using AnalyzeFile.Model.Extensions;

namespace AnalyzeFile.Core.AnalyzeStrategy;

public class ReadAllLinesWithForStrategy : IAnalyzeFileStrategy
{
    public List<(string, string)> AnalyzeData(string pathToReport)
    {
        var results = new List<(string, string)>();
        var usersDocumentsAccess = new Dictionary<int, Dictionary<int, bool>>();

        var lines = File.ReadAllLines(pathToReport);

        for (var index = lines.Length - 1; index >= 0; index--)
        {
            var line = lines[index];
            var lineResult = line.GetDateFromLine();
            if (!lineResult.IsSuccess)
            {
                continue;
            }

            if (usersDocumentsAccess.ContainsKey(lineResult.UserId))
            {
                if (!usersDocumentsAccess[lineResult.UserId].TryAdd(lineResult.DocumentId, false) &&
                    !usersDocumentsAccess[lineResult.UserId][lineResult.DocumentId])
                {
                    usersDocumentsAccess[lineResult.UserId][lineResult.DocumentId] = true;
                    results.Add((lineResult.UserId.ToString(), lineResult.DocumentId.ToString()));
                }
            }
            else
            {
                usersDocumentsAccess[lineResult.UserId] = new Dictionary<int, bool>
                    { { lineResult.DocumentId, false } };
            }
        }

        return results;
    }
}