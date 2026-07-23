using AnalyzeFile.Core;
using AnalyzeFile.Core.AnalyzeStrategy;
using BenchmarkDotNet.Attributes;
using SampleData;

namespace AnalyzeFile.Benchmark.Benchmarks;

[MemoryDiagnoser]
public class AnalyzerMediumFile
{
    public static readonly string TodayReportPath = Paths.TodayReportMediumPath;
    public static readonly string YesterdayReportPath = Paths.YesterdayReportMediumPath;

    [Benchmark]
    public async Task<List<(string, string)>> MediumFile_StreamReaderWithReadLineStrategy()
    {
        var fileProcessor = new FileProcessor(new StreamReaderWithReadLineStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> MediumFile_ReadLinesStrategyStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadLinesStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> MediumFile_ReadAllLinesWithForeachStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadAllLinesWithForeachStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> MediumFile_ReadAllLinesWithForStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadAllLinesWithForStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> MediumFile_StringSplitStrategy()
    {
        var fileProcessor = new FileProcessor(new StringSplitStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }
}
