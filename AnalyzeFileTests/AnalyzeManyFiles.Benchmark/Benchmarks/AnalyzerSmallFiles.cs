using AnalyzeFile.Core.AnalyzeStrategy;
using AnalyzeManyFiles.Core;
using BenchmarkDotNet.Attributes;
using SampleData;

namespace AnalyzeManyFiles.Benchmark.Benchmarks;

public class AnalyzerSmallFiles
{
    private static readonly string TodayReportPath = Paths.TodayReportSmallPath;
    private static readonly string YesterdayReportPath = Paths.TodayReportSmallPath;

    [Benchmark]
    public void SmallFile_SharedLock()
    {
        var fileProcessor = new FileProcessorTaskSharedLock();
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public void SmallFile_SeparateAnalyze()
    {
        var fileProcessor = new FileProcessorTaskSeparate(new StreamReaderWithReadLineStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public void SmallFile_TaskSeparateWithUnionWith()
    {
        var fileProcessor = new FileProcessorTaskSeparateWithUnionWith(new StreamReaderWithReadLineStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public void SmallFile_SeparateWithConcatAndDistinct()
    {
        var fileProcessor = new FileProcessorTaskSeparateWithConcatAndDistinct(new StreamReaderWithReadLineStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }
    
    [Benchmark]
    public void SmallFile_SharedHashSet()
    {
        var fileProcessor = new FileProcessorTaskWIthSharedHashSet(new StreamReaderWithReadLineStrategyWithSharedHashSet());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }
}