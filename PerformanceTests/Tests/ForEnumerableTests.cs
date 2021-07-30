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
    public class ForEnumerableTests
    {
        [Params(1, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 50000, 100000, 500000, 1000000, int.MaxValue)]
        public int Size { get; set; }
        public List<int> TestEnumeration { get; set; }
        
        public ForEnumerableTests()
        {
            TestEnumeration = new List<int>(Size);

            for (int i = 0; i < TestEnumeration.Count; i++)
            {
                TestEnumeration[i] = int.MaxValue;
            }
        }

        [Benchmark]
        public void Test_For()
        {
            long sum = 0;
            for (int i = 0; i < TestEnumeration.Count; i++)
            {
                sum += TestEnumeration[i];
            }
        }

        [Benchmark]
        public void Test_For_With_ToList()
        {
            long sum = 0;
            var a = TestEnumeration.ToList();
            for (int i = 0; i < a.Count; i++)
            {
                sum += a[i];
            }
        }
        
        [Benchmark]
        public void Test_ForRevers()
        {
            long sum = 0;
            for (int i = TestEnumeration.Count - 1; i >= 0; i--)
            {
                sum += TestEnumeration[i];
            }
        }
        
        [Benchmark]
        public void Test_Foreach()
        {
            long sum = 0;
            foreach (var i in TestEnumeration)
            {
                sum += i;
            }
        }
    }

}