namespace AnalyzeFile.Core.DataGeneration;

public class FileGenerator : IFileGenerator
{
    public const string TodayReportDefaultName = "todayReport.csv";
    public int UserIdMinValue { get; set; }
    public int UserIdMaxValue { get; set; }

    public int DocumentIdMinValue { get; set; }
    public int DocumentIdMaxValue { get; set; }

    public FileGenerator(int userIdMinValue = 1, int userIdMaxValue = 50, int documentIdMinValue = 1, int documentIdMaxValue = 50)
    {
        UserIdMinValue = userIdMinValue;
        UserIdMaxValue = userIdMaxValue;
        DocumentIdMinValue = documentIdMinValue;
        DocumentIdMaxValue = documentIdMaxValue;
    }

    public void PrepareFile(string targetFilePath, int numberOfRecords = 1000, DateOnly? dateOnly = null, bool includeHeader = false)
    {
        targetFilePath = string.IsNullOrEmpty(targetFilePath) || string.IsNullOrWhiteSpace(targetFilePath)
            ? TodayReportDefaultName
            : targetFilePath;

        dateOnly ??= DateOnly.FromDateTime(DateTime.Now);
        var today = dateOnly.Value.ToString();

        // Create a new file or overwrite an existing one
        using (var writer = new StreamWriter(targetFilePath))
        {
            if (includeHeader)
            {
                writer.WriteLine("timeSpan,UserId,DocumentId");
            }
            
            var rand = new Random();
            for (var i = 0; i < numberOfRecords; i++)
            {
                var userId = rand.Next(UserIdMinValue, UserIdMaxValue + 1);
                var documentId = rand.Next(DocumentIdMinValue, DocumentIdMaxValue + 1);
                
                writer.WriteLine($"{today},{userId},{documentId}");
            }
        }

        Console.WriteLine("Data generated and saved to " + targetFilePath);
    }
}