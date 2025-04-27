namespace AnalyzeFile.Interface;

public interface IFileProcessor
{
    Task<List<(string, string)>> AnalyzeFiles(string pathToTodayReport, string pathToYesterdayReport);
}