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
    public class PointerTests
    {
        [Params(1, 5, 10, 20, 50, 100, 200, 10000, 50000, 1000000)]
        public int ArraySize { get; set; }

        public int[] Array { get; set; }

        public PointerTests()
        {
            Array = new int[ArraySize];

            for (int i = 0; i < ArraySize; i++)
            {
                Array[i] = 139;
            }
        }

        [Benchmark]
        public void SafeAddValue()
        {
            for (int i = 0; i < Array.Length; i++)
            {
                Array[i] = (Array[i] * Array[i] * 5) / Array[i] + Array[i];
            }
        }

        [Benchmark]
        public void UnsafeAddValue()
        {
            unsafe
            {
                fixed (int* start = Array)
                {
                    int* p = start;
                    var stopAddress = (int) p + Array.Length * sizeof(int);
                    while ((int) p != stopAddress)
                    {
                        *p = (*p * *p * 5) / *p + *p;
                        p++;
                    }
                }
            }
        }

        [Benchmark]
        public void SafeImageCalculation()
        {
            byte[] image = new byte[ArraySize * ArraySize * 3];

            for (int i = 0; i < image.Length;)
            {
                byte grey = (byte)(.299 * image [i + 2] + .587 * image [i + 1] + .114 * image [i]);
                image [i] = grey;
                image [i + 1] = grey;
                image [i + 2] = grey;
                i += 3;
            }
        }
        

        [Benchmark]
        public void UnsafeImageCalculation()
        {
            byte[] image = new byte[ArraySize * ArraySize * 3];

            unsafe
            {
                fixed (byte* imgPtr = &image[0])
                {
                    byte* p = imgPtr;
                    int stopAddress = (int) p + ArraySize * ArraySize * 3;
                    while ((int) p != stopAddress)
                    {
                        byte grey = (byte) (.299 * p[2] + .587 * p[1] + .114 * p[0]);
                        *p = grey;
                        *(p + 1) = grey;
                        *(p + 2) = grey;
                        p += 3;
                    }
                }
            }
        }
    }
}