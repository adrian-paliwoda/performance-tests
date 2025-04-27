namespace AnalyzeFile.Interface;

public interface IAnalyzeFileStrategyWithSharedHashSet
{
    public void AnalyzeData(string pathToReport, HashSet<(string,string)> sharedHashSet);
}