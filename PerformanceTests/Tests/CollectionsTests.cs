using System.Collections;
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

    public class CollectionsTests
    {
        [Params(1, 10, 100, 1000, 1000000)]
        public int Size { get; set; }
        
        public int[] Array { get; set; }
        public List<int> Enumerable { get; set; }
        public ArrayList ArrayList { get; set; }

        public CollectionsTests()
        {
            Array = new int[Size];
            Enumerable = new List<int>(Size);
            ArrayList = new ArrayList(Size);
            
            for (int i = 0; i < Size; i++)
            {
                Array[i] = i;
                Enumerable.Add(i);
                ArrayList.Add(i);
            }
        }

        [Benchmark]
        public void ArrayAddDigit()
        {
            for (int i = 0; i < Array.Length; i++)
            {
                Array[i] += i;
            }
        }
        
        [Benchmark]
        public void EnumerableAddDigit()
        {
            for (int i = 0; i < Enumerable.Count; i++)
            {
                Enumerable[i] += i;
            }
        }
        
        [Benchmark]
        public void ArrayListAddDigit()
        {
            for (int i = 0; i < ArrayList.Count; i++)
            {
                ArrayList[i] = (int)ArrayList[i] + i;
            }
        }
    }
}