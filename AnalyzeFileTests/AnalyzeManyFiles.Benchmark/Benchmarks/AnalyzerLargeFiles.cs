using AnalyzeFile.Core.AnalyzeStrategy;
using AnalyzeManyFiles.Core;
using BenchmarkDotNet.Attributes;
using SampleData;

namespace AnalyzeManyFiles.Benchmark.Benchmarks;

public class AnalyzerLargeFiles
{
    public static readonly string TodayReportPath = Paths.TodayReportLargePath;
    public static readonly string YesterdayReportPath = Paths.TodayReportLargePath;

    [Benchmark]
    public void LargeFile_SharedLock()
    {
        var fileProcessor = new FileProcessorTaskSharedLock();
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public void LargeFile_SeparateAnalyze()
    {
        var fileProcessor = new FileProcessorTaskSeparate(new StreamReaderWithReadLineStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public void LargeFile_TaskSeparateWithUnionWith()
    {
        var fileProcessor = new FileProcessorTaskSeparateWithUnionWith(new StreamReaderWithReadLineStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public void LargeFile_SeparateWithConcatAndDistinct()
    {
        var fileProcessor = new FileProcessorTaskSeparateWithConcatAndDistinct(new StreamReaderWithReadLineStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }
    
    [Benchmark]
    public void LargeFile_SharedHashSet()
    {
        var fileProcessor = new FileProcessorTaskWIthSharedHashSet(new StreamReaderWithReadLineStrategyWithSharedHashSetStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }
    
    [Benchmark]
    public void LargeFile_SharedConcurrentDictionary()
    {
        var fileProcessor = new FileProcessorTaskWIthSharedConcurrentDictionary(new StreamReaderWithReadLineStrategyWithSharedConcurrentDictionaryStrategy());
        _ = fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }
}