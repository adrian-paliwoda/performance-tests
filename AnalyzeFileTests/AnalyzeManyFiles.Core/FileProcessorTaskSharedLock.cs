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

        var results = new List<(string, string)>();
        var usersDocumentsAccess = new Dictionary<int, Dictionary<int, bool>>();

        await Task.Run(() =>
        {
            using (var streamReader = new StreamReader(pathToTodayReport))
            {
                while (streamReader.ReadLine() is { } singleLine)
                {
                    var lineResult = singleLine.GetDateFromLine();
                    if (!lineResult.IsSuccess)
                    {
                        continue;
                    }

                    lock (results)
                    {
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
            }
            
        } );

        await Task.Run(() =>
        {
            using (var streamReader = new StreamReader(pathToYesterdayReport))
            {
                while (streamReader.ReadLine() is { } singleLine)
                {
                    var lineResult = singleLine.GetDateFromLine();
                    if (!lineResult.IsSuccess)
                    {
                        continue;
                    }

                    lock (results)
                    {
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
            }
            
        } );
        
        return results;
    }
}