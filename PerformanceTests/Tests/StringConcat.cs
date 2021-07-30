using System;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace PerformanceTests.Tests
{
    [CsvMeasurementsExporter]
    [HtmlExporter]
    [PlainExporter]
    [RPlotExporter]
    [HardwareCounters(
        HardwareCounter.BranchMispredictions,
        HardwareCounter.BranchInstructions)]
    [MemoryDiagnoser]
    public class StringConcat
    {
        public static int Length { get; } = 50;

        [Params(1, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000)]
        public int NumberOfElements { get; set; }

        public string Result { get; set; }

        private const string Example = "ExampleString";

        private static readonly Random Random = new();

        [Benchmark]
        public void Plus()
        {
            Result = Example;
            for (var i = 0; i < NumberOfElements; i++)
            {
                Result += RandomString();
            }
        }


        [Benchmark]
        public void StringConcatMethod()
        {
            Result = Example;
            for (var i = 0; i < NumberOfElements; i++)
            {
                Result = string.Concat(Result, RandomString());
            }

        }   
     
        [Benchmark]
        public void StringInterpolationMethod()
        {
            Result = Example;
            for (var i = 0; i < NumberOfElements; i++)
            {
                Result = $"{Result}{RandomString()}";
            }
        }

        [Benchmark]
        public void StringBuilderUsage()
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < NumberOfElements; i++)
            {
                stringBuilder.Append(RandomString());
            }

            Result = stringBuilder.ToString();
        }

        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, Length)
                                        .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }

}