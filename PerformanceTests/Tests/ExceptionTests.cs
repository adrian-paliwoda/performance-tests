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
    public class ExceptionTests
    {
        public List<string> Digits { get; set; }

        [Params(1, 10, 100, 1000, 100000)]
        public int Iterations { get; set; }

        [IterationSetup]
        public void IterationSetup()
        {
            Digits = new List<string>();
            for (var i = 0; i < Iterations; i++)
            {
                Digits.Add(i.ToString());
            }
        }

        [Benchmark]
        public void CalculateWithoutException()
        {
            for (var i = 0; i < Digits.Count; i++)
            {
                var a = i + Digits.Count + 2;
            }
        }
        
        [Benchmark]
        public void CalculateWithExceptionCatch()
        {
            for (var i = 0; i < Digits.Count; i++)
            {
                try
                {
                    var a = i + Digits.Count + 2;
                }
                catch (Exception e)
                {
                    ;
                }
                
            }
        }
        
        [Benchmark]
        public void CalculateWithExceptionCatchAndThrowing()
        {
            for (var i = 0; i < Digits.Count; i++)
            {
                try
                {
                    var a = i + Digits.Count + 2;

                    throw new AggregateException("example");
                }
                catch (Exception e)
                {
                    ;
                }
                
            }
        }
    }

}