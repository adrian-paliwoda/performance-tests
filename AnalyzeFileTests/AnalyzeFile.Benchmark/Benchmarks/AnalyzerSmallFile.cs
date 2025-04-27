using AnalyzeFile.Core;
using AnalyzeFile.Core.AnalyzeStrategy;
using BenchmarkDotNet.Attributes;
using SampleData;

namespace AnalyzeFile.Benchmark.Benchmarks;

public class AnalyzerSmallFile
{
    public static readonly string TodayReportPath = Paths.TodayReportSmallPath;
    public static readonly string YesterdayReportPath = Paths.TodayReportSmallPath;

    [Benchmark]
    public void SmallFile_StreamReaderWithReadLineStrategy()
    {
        var fileProcessor = new FileProcessor(new StreamReaderWithReadLineStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public void SmallFile_ReadLinesStrategyStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadLinesStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public void SmallFile_ReadAllLinesWithForeachStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadAllLinesWithForeachStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public void SmallFile_ReadAllLinesWithForStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadAllLinesWithForStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public void SmallFile_StringSplitStrategy()
    {
        var fileProcessor = new FileProcessor(new StringSplitStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }
}