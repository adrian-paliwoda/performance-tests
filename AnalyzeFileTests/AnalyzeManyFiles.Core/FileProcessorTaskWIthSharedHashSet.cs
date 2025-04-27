using AnalyzeFile.Interface;

namespace AnalyzeManyFiles.Core;

public class FileProcessorTaskWIthSharedHashSet : IFileProcessor
{
    private readonly IAnalyzeFileStrategyWithSharedHashSet _analyzeFileStrategy;

    public FileProcessorTaskWIthSharedHashSet(IAnalyzeFileStrategyWithSharedHashSet analyzeFileStrategy)
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

        var sharedHashSet = new HashSet<(string,string)>();
        var task0 = Task.Run(() => _analyzeFileStrategy.AnalyzeData(pathToTodayReport, sharedHashSet));
        var task1 = Task.Run(() => _analyzeFileStrategy.AnalyzeData(pathToYesterdayReport, sharedHashSet));

        await Task.WhenAll(task0, task1);

        return sharedHashSet.ToList();
    }
}