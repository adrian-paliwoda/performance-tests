// See https://aka.ms/new-console-template for more information

using AnalyzeFile.Core;
using AnalyzeFile.Core.AnalyzeStrategy;
using AnalyzeFile.Model.Extensions;
using SampleData;

Console.WriteLine("Application started!");

// var fileGenerator = new FileGenerator();
// // fileGenerator.PrepareFile(null, 100000000);
// int small = 10;
// int medium = 100000;
// int large = 100000000;
//
//
// fileGenerator.PrepareFile("yesterdayReport.csv", large, DateOnly.FromDateTime(DateTime.Now).AddDays(-1));

var fileProcessor0 = new FileProcessor(new StreamReaderWithReadLineStrategy());
var fileProcessor1 = new FileProcessor(new ReadLinesStrategy());
var fileProcessor2 = new FileProcessor(new ReadAllLinesWithForeachStrategy());
var fileProcessor3 = new FileProcessor(new ReadAllLinesWithForStrategy());
var fileProcessor4 = new FileProcessor(new StringSplitStrategy());

var result = await fileProcessor0.AnalyzeFiles(Paths.TodayReportMediumPath, Paths.YesterdayReportMediumPath);
result.ShowInConsole();