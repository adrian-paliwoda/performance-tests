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
    public class ForArrayTests
    {
        [Params(1, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 50000, 100000, 500000, 1000000, int.MaxValue)]
        public int Size { get; set; }
        public int[] TestArray { get; set; }

        public ForArrayTests()
        {
            TestArray = new int[Size];

            for (int i = 0; i < TestArray.Length; i++)
            {
                TestArray[i] = int.MaxValue;
            }
        }

        [Benchmark]
        public void Test_For()
        {
            long sum = 0;
            for (int i = 0; i < TestArray.Length; i++)
            {
                sum += TestArray[i];
            }
        }
        
        [Benchmark]
        public void Test_ForRevers()
        {
            long sum = 0;
            for (int i = TestArray.Length - 1; i >= 0; i--)
            {
                sum += TestArray[i];
            }
        }
        
        [Benchmark]
        public void Test_Foreach()
        {
            long sum = 0;
            foreach (var i in TestArray)
            {
                sum += i;
            }
        }
    }

}