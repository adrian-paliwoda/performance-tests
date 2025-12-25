using AnalyzeFile.Interface;
using AnalyzeFile.Model.Extensions;

namespace AnalyzeFile.Core.AnalyzeStrategy;

public class StreamReaderWithReadLineStrategyWithSharedHashSetStrategy : IAnalyzeFileStrategyWithSharedHashSet
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