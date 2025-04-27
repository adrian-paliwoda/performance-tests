using AnalyzeFile.Core;
using AnalyzeFile.Core.AnalyzeStrategy;
using BenchmarkDotNet.Attributes;
using SampleData;

namespace AnalyzeFile.Benchmark.Benchmarks;

public class AnalyzerMediumFile
{
    public static string todayReportPath = Paths.TodayReportMediumPath;
    public static string yestardayReportPath = Paths.TodayReportMediumPath;

    [Benchmark]
    public void MediumFile_StreamReaderWithReadLineStrategy()
    {
        var fileProcessor = new FileProcessor(new StreamReaderWithReadLineStrategy());
        var result = fileProcessor.AnalyzeFiles(todayReportPath, yestardayReportPath);
    }

    [Benchmark]
    public void MediumFile_ReadLinesStrategyStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadLinesStrategy());
        var result = fileProcessor.AnalyzeFiles(todayReportPath, yestardayReportPath);
    }

    [Benchmark]
    public void MediumFile_ReadAllLinesWithForeachStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadAllLinesWithForeachStrategy());
        var result = fileProcessor.AnalyzeFiles(todayReportPath, yestardayReportPath);
    }

    [Benchmark]
    public void MediumFile_ReadAllLinesWithForStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadAllLinesWithForStrategy());
        var result = fileProcessor.AnalyzeFiles(todayReportPath, yestardayReportPath);
    }

    [Benchmark]
    public void MediumFile_StringSplitStrategy()
    {
        var fileProcessor = new FileProcessor(new StringSplitStrategy());
        var result = fileProcessor.AnalyzeFiles(todayReportPath, yestardayReportPath);
    }
}