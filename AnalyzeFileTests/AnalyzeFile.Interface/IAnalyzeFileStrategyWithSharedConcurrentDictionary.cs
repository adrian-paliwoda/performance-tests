using System.Collections.Concurrent;

namespace AnalyzeFile.Interface;

public interface IAnalyzeFileStrategyWithSharedConcurrentDictionary
{
    public void AnalyzeData(string pathToReport, ConcurrentDictionary<(string,string), bool> sharedConcurrentDictionary);
}