using System;
using System.Collections.Generic;
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
    public class CatchExceptionTests
    {
        public List<string> Digits { get; set; }
        public List<string> NoDigits { get; set; }

        [Params(1, 10, 100, 1000, 100000)]
        public int Iterations { get; set; }

        [IterationSetup]
        public void IterationSetup()
        {
            Digits = new List<string>();
            NoDigits = new List<string>();
            
            for (var i = 0; i < Iterations; i++)
            {
                Digits.Add(i.ToString());
                NoDigits.Add("ZZ");
            }
        }

        [Benchmark]
        public void Calculate_WithoutTryCatch_CorrectData()
        {
            for (var i = 0; i < Digits.Count; i++)
            {
                int result;
                int.TryParse(Digits[i], out result);
            }
        }

        [Benchmark]
        public void Calculate_WithTryCatch_CorrectData()
        {
            for (var i = 0; i < Digits.Count; i++)
            {
                try
                {
                    var result = int.Parse(Digits[i]);
                }
                catch (Exception e)
                {
                    ;
                }
            }
        }
        
        [Benchmark]
        public void Calculate_WithoutTryCatch_WrongData()
        {
            for (var i = 0; i < NoDigits.Count; i++)
            {
                int result;
                int.TryParse(NoDigits[i], out result);
            }
        }

        [Benchmark]
        public void Calculate_WithTryCatch_WrongData()
        {
            for (var i = 0; i < NoDigits.Count; i++)
            {
                try
                {
                    var result = int.Parse(NoDigits[i]);
                }
                catch (Exception e)
                {
                    ;
                }
            }
        }
    }

}