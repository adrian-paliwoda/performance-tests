using AnalyzeFile.Core;
using AnalyzeFile.Core.AnalyzeStrategy;
using BenchmarkDotNet.Attributes;
using SampleData;

namespace AnalyzeFile.Benchmark.Benchmarks;

public class AnalyzerMediumFile
{
    public static readonly string TodayReportPath = Paths.TodayReportMediumPath;
    public static readonly string YesterdayReportPath = Paths.TodayReportMediumPath;

    [Benchmark]
    public void MediumFile_StreamReaderWithReadLineStrategy()
    {
        var fileProcessor = new FileProcessor(new StreamReaderWithReadLineStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public void MediumFile_ReadLinesStrategyStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadLinesStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public void MediumFile_ReadAllLinesWithForeachStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadAllLinesWithForeachStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public void MediumFile_ReadAllLinesWithForStrategy()
    {
        var fileProcessor = new FileProcessor(new ReadAllLinesWithForStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public void MediumFile_StringSplitStrategy()
    {
        var fileProcessor = new FileProcessor(new StringSplitStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }
}