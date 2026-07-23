using AnalyzeFile.Core.AnalyzeStrategy;
using AnalyzeManyFiles.Core;
using BenchmarkDotNet.Attributes;
using SampleData;

namespace AnalyzeManyFiles.Benchmark.Benchmarks;

[MemoryDiagnoser]
public class AnalyzerSmallFiles
{
    private static readonly string TodayReportPath = Paths.TodayReportSmallPath;
    private static readonly string YesterdayReportPath = Paths.YesterdayReportSmallPath;

    [Benchmark]
    public async Task<List<(string, string)>> SmallFile_SharedLock()
    {
        var fileProcessor = new FileProcessorTaskSharedLock();
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> SmallFile_SeparateAnalyze()
    {
        var fileProcessor = new FileProcessorTaskSeparate(new StreamReaderWithReadLineStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> SmallFile_TaskSeparateWithUnionWith()
    {
        var fileProcessor = new FileProcessorTaskSeparateWithUnionWith(new StreamReaderWithReadLineStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> SmallFile_SeparateWithConcatAndDistinct()
    {
        var fileProcessor = new FileProcessorTaskSeparateWithConcatAndDistinct(new StreamReaderWithReadLineStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> SmallFile_SharedHashSet()
    {
        var fileProcessor = new FileProcessorTaskWIthSharedHashSet(new StreamReaderWithReadLineStrategyWithSharedHashSetStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> SmallFile_SharedConcurrentDictionary()
    {
        var fileProcessor = new FileProcessorTaskWIthSharedConcurrentDictionary(new StreamReaderWithReadLineStrategyWithSharedConcurrentDictionaryStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }
}
