using AnalyzeFile.Core;
using AnalyzeFile.Core.AnalyzeStrategy;
using BenchmarkDotNet.Attributes;
using SampleData;

namespace AnalyzeFile.Benchmark.Benchmarks;

[MemoryDiagnoser]
public class AnalyzerSmallFile
{
    public static readonly string TodayReportPath = Paths.TodayReportSmallPath;
    public static readonly string YesterdayReportPath = Paths.YesterdayReportSmallPath;

    [Benchmark]
    public async Task<List<(string, string)>> SmallFile_StreamReaderWithReadLineStrategy()
    {
        var fileProcessor = new FileProcessor(new StreamReaderWithReadLineStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> SmallFile_ReadLinesStrategyStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadLinesStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> SmallFile_ReadAllLinesWithForeachStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadAllLinesWithForeachStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> SmallFile_ReadAllLinesWithForStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadAllLinesWithForStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> SmallFile_StringSplitStrategy()
    {
        var fileProcessor = new FileProcessor(new StringSplitStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }
}
