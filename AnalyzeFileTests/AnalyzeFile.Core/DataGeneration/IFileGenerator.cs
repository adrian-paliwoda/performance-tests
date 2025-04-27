using System;

namespace AnalyzeFile.Core.DataGeneration;

public interface IFileGenerator
{
    void PrepareFile(string targetFilePath, int numberOfRecords = 1000, DateOnly? dateOnly = null, bool includeHeader = false);
}