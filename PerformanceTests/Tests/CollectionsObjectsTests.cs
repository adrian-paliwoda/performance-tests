using System.Collections;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using PerformanceTests.Models;

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

    public class CollectionsObjectsTests
    {
        [Params(1, 10, 100, 1000, 1000000)]
        public int Size { get; set; }
        
        public Book[] Array { get; set; }
        public List<Book> Enumerable { get; set; }
        public ArrayList ArrayList { get; set; }

        public CollectionsObjectsTests()
        {
            Array = new Book[Size];
            Enumerable = new List<Book>(Size);
            ArrayList = new ArrayList(Size);
            
            for (int i = 0; i < Size; i++)
            {
                Array[i] = new Book();
                Enumerable.Add(new Book());
                ArrayList.Add(new Book());
            }
        }

        [Benchmark]
        public void ArrayAddDigit()
        {
            for (int i = 0; i < Array.Length; i++)
            {
                Array[i].NumberOfPages += i;
            }
        }
        
        [Benchmark]
        public void EnumerableAddDigit()
        {
            for (int i = 0; i < Enumerable.Count; i++)
            {
                Enumerable[i].NumberOfPages += i;
            }
        }
        
        [Benchmark]
        public void ArrayListAddDigit()
        {
            for (int i = 0; i < ArrayList.Count; i++)
            {
                ((Book)ArrayList[i]).NumberOfPages += i;
            }
        }
    } 
}