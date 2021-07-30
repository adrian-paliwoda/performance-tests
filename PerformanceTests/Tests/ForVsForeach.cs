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
    public class ForVsForeach
    {
        [Params(1, 10, 100, 1000, 1000000)]
        public int Size { get; set; }
        
        public int[] Array { get; set; }
        public List<int> Enumerable { get; set; }
        public ArrayList ArrayList { get; set; }

        public ForVsForeach()
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
        public void ArrayAddDigitFor()
        {
            var suma = 0;
            for (int i = 0; i < Array.Length; i++)
            {
                suma += Array[i];
            }
        }
        
        [Benchmark]
        public void EnumerableAddDigitFor()
        {
            var suma = 0;
            for (int i = 0; i < Enumerable.Count; i++)
            {
                suma += Enumerable[i];
            }
        }
        
        [Benchmark]
        public void ArrayListAddDigitFor()
        {
            var suma = 0;
            for (int i = 0; i < ArrayList.Count; i++)
            {
                suma += (int)ArrayList[i];
            }
        }
        
        [Benchmark]
        public void ArrayAddDigitForeach()
        {
            var suma = 0;
            foreach (var item in Array)
            {
                suma += item;
            }
        }
        
        [Benchmark]
        public void EnumerableAddDigitForeach()
        {
            var suma = 0;
            foreach (var item in Enumerable)
            {
                suma += item;
            }
        }
        
        [Benchmark]
        public void ArrayListAddDigitForeach()
        {
            var suma = 0;
            foreach (var item in ArrayList)
            {
                suma += (int)item;
            }
        }
    }
}