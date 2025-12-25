using System.Collections.Concurrent;
using AnalyzeFile.Interface;
using AnalyzeFile.Model.Extensions;

namespace AnalyzeFile.Core.AnalyzeStrategy;

public class StreamReaderWithReadLineStrategyWithSharedConcurrentDictionaryStrategy : IAnalyzeFileStrategyWithSharedConcurrentDictionary
{
    public void AnalyzeData(string pathToReport, ConcurrentDictionary<(string, string), bool> sharedHashSet)
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
                            sharedHashSet.TryAdd((lineResult.UserId.ToString(), lineResult.DocumentId.ToString()), true);
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