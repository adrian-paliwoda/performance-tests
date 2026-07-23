using AnalyzeFile.Interface;
using AnalyzeFile.Model.Extensions;

namespace AnalyzeManyFiles.Core;

public class FileProcessorTaskSharedLock : IFileProcessor
{
    public async Task<List<(string, string)>> AnalyzeFiles(string pathToTodayReport, string pathToYesterdayReport)
    {
        if (string.IsNullOrEmpty(pathToTodayReport) || string.IsNullOrWhiteSpace(pathToTodayReport) ||
            !File.Exists(pathToTodayReport))
        {
            Console.WriteLine("Please provide valid file path for today report: " + pathToTodayReport);
            return [];
        }

        if (string.IsNullOrEmpty(pathToYesterdayReport) || string.IsNullOrWhiteSpace(pathToYesterdayReport) ||
            !File.Exists(pathToYesterdayReport))
        {
            Console.WriteLine("Please provide valid file path for yesterday report" + pathToTodayReport);
            return [];
        }

        var results = new HashSet<(string, string)>();

        var task0 = Task.Run(() => AnalyzeSingleFile(pathToTodayReport, results));
        var task1 = Task.Run(() => AnalyzeSingleFile(pathToYesterdayReport, results));

        await Task.WhenAll(task0, task1);

        return results.ToList();
    }

    private static void AnalyzeSingleFile(string pathToReport, HashSet<(string, string)> results)
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
                        lock (results)
                        {
                            results.Add((lineResult.UserId.ToString(), lineResult.DocumentId.ToString()));
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