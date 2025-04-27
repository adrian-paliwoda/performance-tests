namespace SampleData;

public static class Paths
{
    public const string YesterdayReportLargeFileName = "yesterdayReport_large.csv";
    public const string YesterdayReportSmallFileName = "yesterdayReport_small.csv";
    public const string YesterdayReportMediumFileName = "yesterdayReport_medium.csv";

    public const string TodayReportLargeFileName = "todayReport_large.csv";
    public const string TodayReportSmallFileName = "todayReport_small.csv";
    public const string TodayReportMediumFileName = "todayReport_medium.csv";

    public const string SampleDataDirectoryName = "SampleData";

    public static string TodayReportPath => TodayReportMediumPath;
    public static string YesterdayReportPath => YesterdayReportMediumPath;

    public static string TodayReportSmallPath
    {
        get
        {
            var fullName = GetSolutionPath()?.FullName;
            if (!string.IsNullOrEmpty(fullName) && !string.IsNullOrWhiteSpace(fullName))
            {
                return Path.Combine(fullName, SampleDataDirectoryName, TodayReportSmallFileName);
            }

            return AppContext.BaseDirectory;
        }
    }

    public static string TodayReportMediumPath
    {
        get
        {
            var fullName = GetSolutionPath()?.FullName;
            if (!string.IsNullOrEmpty(fullName) && !string.IsNullOrWhiteSpace(fullName))
            {
                return Path.Combine(fullName, SampleDataDirectoryName, TodayReportMediumFileName);
            }

            return AppContext.BaseDirectory;
        }
    }

    public static string TodayReportLargePath
    {
        get
        {
            var fullName = GetSolutionPath()?.FullName;
            if (!string.IsNullOrEmpty(fullName) && !string.IsNullOrWhiteSpace(fullName))
            {
                return Path.Combine(fullName, SampleDataDirectoryName, TodayReportLargeFileName);
            }

            return AppContext.BaseDirectory;
        }
    }

    public static string YesterdayReportSmallPath
    {
        get
        {
            var fullName = GetSolutionPath()?.FullName;
            if (!string.IsNullOrEmpty(fullName) && !string.IsNullOrWhiteSpace(fullName))
            {
                return Path.Combine(fullName, SampleDataDirectoryName, YesterdayReportSmallFileName);
            }

            return AppContext.BaseDirectory;
        }
    }

    public static string YesterdayReportMediumPath
    {
        get
        {
            var fullName = GetSolutionPath()?.FullName;
            if (!string.IsNullOrEmpty(fullName) && !string.IsNullOrWhiteSpace(fullName))
            {
                return Path.Combine(fullName, SampleDataDirectoryName, YesterdayReportMediumFileName);
            }

            return AppContext.BaseDirectory;
        }
    }

    public static string YesterdayReportLargePath
    {
        get
        {
            var fullName = GetSolutionPath()?.FullName;
            if (!string.IsNullOrEmpty(fullName) && !string.IsNullOrWhiteSpace(fullName))
            {
                return Path.Combine(fullName, SampleDataDirectoryName, YesterdayReportLargeFileName);
            }

            return AppContext.BaseDirectory;
        }
    }

    private static DirectoryInfo? GetSolutionPath()
    {
        var index = AppContext.BaseDirectory.IndexOf(Path.DirectorySeparatorChar + "bin" +
                                                     Path.DirectorySeparatorChar, StringComparison.Ordinal);
        return index >= 0
            ? Directory.GetParent(AppContext.BaseDirectory.Substring(0, index))
            : Directory.GetParent(AppContext.BaseDirectory);
    }
}