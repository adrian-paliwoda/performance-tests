using System.Collections.Generic;
using System.IO;
using AnalyzeFile.Interface;
using AnalyzeFile.Model.Extensions;

namespace AnalyzeFile.Core.AnalyzeStrategy;

public class StreamReaderWithReadLineStrategy : IAnalyzeFileStrategy
{
    public List<(string, string)> AnalyzeData(string pathToReport)
    {
        var results = new List<(string, string)>();
        var usersDocumentsAccess = new Dictionary<int, Dictionary<int, bool>>();

        using (var streamReader = new StreamReader(pathToReport))
        {
            while (streamReader.ReadLine() is { } singleLine)
            {
                var lineResult = singleLine.GetDateFromLine();
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
        }

        return results;
    }
}

public class StreamReaderWithReadLineStrategyWithSharedHashSet : IAnalyzeFileStrategyWithSharedHashSet
{
    public void AnalyzeData(string pathToReport, HashSet<(string, string)> sharedHashSet)
    {
        var usersDocumentsAccess = new Dictionary<int, Dictionary<int, bool>>();

        using (var streamReader = new StreamReader(pathToReport))
        {
            while (streamReader.ReadLine() is { } singleLine)
            {
                var lineResult = singleLine.GetDateFromLine();
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
                        lock (sharedHashSet)
                        {
                            sharedHashSet.Add((lineResult.UserId.ToString(), lineResult.DocumentId.ToString()));
                        }
                    }
                }
                else
                {
                    usersDocumentsAccess[lineResult.UserId] = new Dictionary<int, bool>
                        { { lineResult.DocumentId, false } };
                }
            }
        }
    }
}