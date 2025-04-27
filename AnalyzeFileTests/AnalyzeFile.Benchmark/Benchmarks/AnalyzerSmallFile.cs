using AnalyzeFile.Core;
using AnalyzeFile.Core.AnalyzeStrategy;
using BenchmarkDotNet.Attributes;
using SampleData;

namespace AnalyzeFile.Benchmark.Benchmarks;

public class AnalyzerSmallFile
{
    public static string todayReportPath = Paths.TodayReportSmallPath;
    public static string yestardayReportPath = Paths.TodayReportSmallPath;

    [Benchmark]
    public void SmallFile_StreamReaderWithReadLineStrategy()
    {
        var fileProcessor = new FileProcessor(new StreamReaderWithReadLineStrategy());
        var result = fileProcessor.AnalyzeFiles(todayReportPath, yestardayReportPath);
    }

    [Benchmark]
    public void SmallFile_ReadLinesStrategyStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadLinesStrategy());
        var result = fileProcessor.AnalyzeFiles(todayReportPath, yestardayReportPath);
    }

    [Benchmark]
    public void SmallFile_ReadAllLinesWithForeachStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadAllLinesWithForeachStrategy());
        var result = fileProcessor.AnalyzeFiles(todayReportPath, yestardayReportPath);
    }

    [Benchmark]
    public void SmallFile_ReadAllLinesWithForStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadAllLinesWithForStrategy());
        var result = fileProcessor.AnalyzeFiles(todayReportPath, yestardayReportPath);
    }

    [Benchmark]
    public void SmallFile_StringSplitStrategy()
    {
        var fileProcessor = new FileProcessor(new StringSplitStrategy());
        var result = fileProcessor.AnalyzeFiles(todayReportPath, yestardayReportPath);
    }
}