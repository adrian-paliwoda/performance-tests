using System.Collections.Concurrent;
using AnalyzeFile.Interface;

namespace AnalyzeManyFiles.Core;

public class FileProcessorTaskWIthSharedConcurrentDictionary : IFileProcessor
{
    private readonly IAnalyzeFileStrategyWithSharedConcurrentDictionary _analyzeFileStrategy;

    public FileProcessorTaskWIthSharedConcurrentDictionary(IAnalyzeFileStrategyWithSharedConcurrentDictionary analyzeFileStrategy)
    {
        ArgumentNullException.ThrowIfNull(analyzeFileStrategy);
        
        _analyzeFileStrategy = analyzeFileStrategy;
    }

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

        var sharedConcurrentDictionary = new ConcurrentDictionary<(string,string), bool>();
        var task0 = Task.Run(() => _analyzeFileStrategy.AnalyzeData(pathToTodayReport, sharedConcurrentDictionary));
        var task1 = Task.Run(() => _analyzeFileStrategy.AnalyzeData(pathToYesterdayReport, sharedConcurrentDictionary));

        await Task.WhenAll(task0, task1);

        return sharedConcurrentDictionary.Keys.ToList();
    }
}