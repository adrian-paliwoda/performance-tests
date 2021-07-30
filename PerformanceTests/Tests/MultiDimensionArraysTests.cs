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

    public class MultiDimensionArraysTests
    {
        [Params(1, 10, 100, 1000, 10000)]
        // The same size for each dimension
        public int Size { get; set; }

        public int[] SingleDim { get; set; }
        public int[,] MultiDim { get; set; } // it's class
        public int[][] Jagged { get; set; } // array in array


        [Benchmark]
        public void SingleDim_FillArray()
        {
            SingleDim = new int[Size * Size];

            for (int i = 0; i < SingleDim.Length; i++)
            {
                SingleDim[i] = i;
            }
        }

        [Benchmark]
        public void MultDim_FillArray()
        {
            MultiDim = new int[Size, Size];

            for (int i = 0; i < MultiDim.GetLength(0); i++)
            {
                for (int j = 0; j < MultiDim.GetLength(1); j++)
                {
                    MultiDim[i, j] = i;
                }
            }
        }

        [Benchmark]
        public void Jagged_FillArray()
        {
            Jagged = new int[Size][];
            for (int i = 0; i < Jagged.Length; i++)
            {
                Jagged[i] = new int[Size];
                for (int j = 0; j < Jagged[i].Length; j++)
                {
                    Jagged[i][j] = i;
                }
            }
        }

        [Benchmark]
        public void SingleDim_AddOneToValue()
        {
            SingleDim_FillArray();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    int index = i + (j * Size);
                    SingleDim[index] += 1;
                }
            }
        } 
        
        [Benchmark]
        public void MultiDim_AddOneToValue()
        {
            MultDim_FillArray();

            for (int i = 0; i < MultiDim.GetLength(0); i++)
            {
                for (int j = 0; j < MultiDim.GetLength(1); j++)
                {
                    MultiDim[i, j] += 1;
                }
            }
        }       
        
        [Benchmark]
        public void Jagged_AddOneToValue()
        {
            Jagged_FillArray();

            for (int i = 0; i < Jagged.Length; i++)
            {
                for (int j = 0; j < Jagged[i].Length; j++)
                {
                    Jagged[i][j] += 1;
                }
            }
        }
        
    }
}