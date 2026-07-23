using AnalyzeFile.Core.AnalyzeStrategy;
using AnalyzeManyFiles.Core;
using BenchmarkDotNet.Attributes;
using SampleData;

namespace AnalyzeManyFiles.Benchmark.Benchmarks;

[MemoryDiagnoser]
public class AnalyzerMediumFiles
{
    private static readonly string TodayReportPath = Paths.TodayReportMediumPath;
    private static readonly string YesterdayReportPath = Paths.YesterdayReportMediumPath;

    [Benchmark]
    public async Task<List<(string, string)>> MediumFile_SharedLock()
    {
        var fileProcessor = new FileProcessorTaskSharedLock();
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> MediumFile_SeparateAnalyze()
    {
        var fileProcessor = new FileProcessorTaskSeparate(new StreamReaderWithReadLineStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> MediumFile_TaskSeparateWithUnionWith()
    {
        var fileProcessor = new FileProcessorTaskSeparateWithUnionWith(new StreamReaderWithReadLineStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> MediumFile_SeparateWithConcatAndDistinct()
    {
        var fileProcessor = new FileProcessorTaskSeparateWithConcatAndDistinct(new StreamReaderWithReadLineStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> Medium_File_SharedConcurrentDictionary()
    {
        var fileProcessor = new FileProcessorTaskWIthSharedConcurrentDictionary(new StreamReaderWithReadLineStrategyWithSharedConcurrentDictionaryStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }

    [Benchmark]
    public async Task<List<(string, string)>> MediumFile_SharedHashSet()
    {
        var fileProcessor = new FileProcessorTaskWIthSharedHashSet(new StreamReaderWithReadLineStrategyWithSharedHashSetStrategy());
        return await fileProcessor.AnalyzeFiles(TodayReportPath, YesterdayReportPath);
    }
}
