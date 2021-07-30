using System.Collections.Generic;
using System.Linq;
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
    public class CountTests
    {
        [Params(1, 5, 10, 20, 50, 100, 10000, 50000, 100000, 500000, 1000000)]
        public int NumberOfElements { get; set; }
        public List<int> IntElements { get; set; }

        [IterationSetup]
        public void IterationSetup()
        {
            IntElements = new List<int>();
            for (int i = 0; i < NumberOfElements; i++)
            {
                IntElements.Add(int.MaxValue);
            }
        }
      
        /// <summary>
        /// Use Count as property
        /// </summary>
        [Benchmark]
        public void CountProperty()
        {
            var result = IntElements.Count;
        }
      
        /// <summary>
        /// Use Count() as method from LINQ
        /// </summary>
        [Benchmark]
        public void CountMethod()
        {
            var result = IntElements.Count();
        }
      
    }
}