using System.Collections.Generic;
using System.IO;
using AnalyzeFile.Interface;
using AnalyzeFile.Model.Extensions;

namespace AnalyzeFile.Core.AnalyzeStrategy;

public class ReadLinesStrategy : IAnalyzeFileStrategy
{
    public List<(string, string)> AnalyzeData(string pathToReport)
    {
        var results = new List<(string, string)>();
        var usersDocumentsAccess = new Dictionary<int, Dictionary<int, bool>>();

        var lines = File.ReadLines(pathToReport);

        foreach (var line in lines)
        {
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