namespace AnalyzeFile.Model.Extensions;

public static class StringExtenstion
{
    public static LineResult GetDateFromLine(this string? singleLine)
    {
        var lineResult = new LineResult();
        if (string.IsNullOrEmpty(singleLine) || string.IsNullOrWhiteSpace(singleLine))
        {
            return lineResult;
        }
        
        var splitLine = singleLine.Split(',');
        if (splitLine.Length != 3
            || !int.TryParse(splitLine[1], out var userId)
            || !int.TryParse(splitLine[2], out var documentId))
        {
            return lineResult;
        }

        lineResult.UserId = userId;
        lineResult.DocumentId = documentId;
        lineResult.IsSuccess = true;

        return lineResult;
    }
}