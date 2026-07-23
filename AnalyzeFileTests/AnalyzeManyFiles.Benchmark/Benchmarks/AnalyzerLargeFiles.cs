using AnalyzeFile.Core.AnalyzeStrategy;
using AnalyzeManyFiles.Core;
using BenchmarkDotNet.Attributes;
using SampleData;

namespace AnalyzeManyFiles.Benchmark.Benchmarks;

[MemoryDiagnoser]
public class AnalyzerLargeFiles
{
    public static readonly string TodayReportPath = Paths.TodayReportLargePath;
    public static readonly string YesterdayReportPath = Paths.YesterdayReportLargePath;

    [Benchmark]
    public async Task<List<(string, string)>> LargeFile_SharedLock()
    {
        var fileProcessor = new FileProcessorTaskSharedLock();
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> LargeFile_SeparateAnalyze()
    {
        var fileProcessor = new FileProcessorTaskSeparate(new StreamReaderWithReadLineStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> LargeFile_TaskSeparateWithUnionWith()
    {
        var fileProcessor = new FileProcessorTaskSeparateWithUnionWith(new StreamReaderWithReadLineStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> LargeFile_SeparateWithConcatAndDistinct()
    {
        var fileProcessor = new FileProcessorTaskSeparateWithConcatAndDistinct(new StreamReaderWithReadLineStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> LargeFile_SharedHashSet()
    {
        var fileProcessor = new FileProcessorTaskWIthSharedHashSet(new StreamReaderWithReadLineStrategyWithSharedHashSetStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> LargeFile_SharedConcurrentDictionary()
    {
        var fileProcessor = new FileProcessorTaskWIthSharedConcurrentDictionary(new StreamReaderWithReadLineStrategyWithSharedConcurrentDictionaryStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }
}
