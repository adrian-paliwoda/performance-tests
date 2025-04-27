using AnalyzeFile.Interface;

namespace AnalyzeManyFiles.Core;

public class FileProcessorTaskSeparateWithConcatAndDistinct : IFileProcessor
{
    private readonly IAnalyzeFileStrategy _analyzeFileStrategy;

    public FileProcessorTaskSeparateWithConcatAndDistinct(IAnalyzeFileStrategy analyzeFileStrategy)
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

        var task0 = Task.Run(() => _analyzeFileStrategy.AnalyzeData(pathToTodayReport));
        var task1 = Task.Run(() => _analyzeFileStrategy.AnalyzeData(pathToYesterdayReport));

        await Task.WhenAll(task0, task1);
        
        var result0 = await task0;
        var result1 = await task1;
        
        return result0.Concat(result1).Distinct().ToList();
    }
}