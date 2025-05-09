﻿namespace AnalyzeFile.Interface;

public interface IAnalyzeFileStrategy
{
    public List<(string, string)> AnalyzeData(string pathToReport);
}