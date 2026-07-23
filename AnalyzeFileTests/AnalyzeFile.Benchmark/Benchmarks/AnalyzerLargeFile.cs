using AnalyzeFile.Core;
using AnalyzeFile.Core.AnalyzeStrategy;
using BenchmarkDotNet.Attributes;
using SampleData;

namespace AnalyzeFile.Benchmark.Benchmarks;

[MemoryDiagnoser]
public class AnalyzerLargeFile
{
    private static readonly string TodayReportPath = Paths.TodayReportLargePath;
    private static readonly string YesterdayReportPath = Paths.YesterdayReportLargePath;

    [Benchmark]
    public async Task<List<(string, string)>> LargeFile_StreamReaderWithReadLineStrategy()
    {
        var fileProcessor = new FileProcessor(new StreamReaderWithReadLineStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> LargeFile_ReadLinesStrategyStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadLinesStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> LargeFile_ReadAllLinesWithForeachStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadAllLinesWithForeachStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> LargeFile_ReadAllLinesWithForStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadAllLinesWithForStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> LargeFile_StringSplitStrategy()
    {
        var fileProcessor = new FileProcessor(new StringSplitStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }
}
