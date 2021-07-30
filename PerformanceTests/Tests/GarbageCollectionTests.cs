using System.Threading;
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
    public class GarbageCollectionTests
    {
        public byte[] SmallHeap { get; set; }
        public byte[] LargeHeap { get; set; }
        public byte[] SomethingToDoWith { get; set; }

        public GarbageCollectionTests()
        {
            SmallHeap = new byte[84999];
            LargeHeap = new byte[85000];
            
            SomethingToDoWith = new byte[55000];
        }
        
        [Benchmark]
        public void LargeObjectInSmallObjectHeap()
        {
            var sum = 0;
            for (int i = 0; i < SmallHeap.Length; i++)
            {
                sum += SmallHeap[i];
                SomethingToDoInMeanTime();
            }
        }
        
        [Benchmark]
        public void LargeObjectInLargeObjectHeap()
        {
            var sum = 0;
            for (int i = 0; i < LargeHeap.Length; i++)
            {
                sum += LargeHeap[i];
            }
        }


        private void SomethingToDoInMeanTime()
        {
            var sum = 0;
            for (int i = 0; i < SomethingToDoWith.Length; i++)
            {
                sum += (byte)(SomethingToDoWith[i] * 6 / 3 + 1);
                Thread.Sleep(1);
            }
        }
    }
}