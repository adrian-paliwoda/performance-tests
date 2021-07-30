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
    public class DelegateTests
    {
        public int[] Array { get; set; }
        public Book BookForManyDelegates { get; set; }
        public Book BookForOneDelegates { get; set; }
        public Book BookForTwoDelegates { get; set; }

        public int NumberOfDelegatesForManyDelegatesTest { get; set; }

        private readonly SimpleDelegate simpleDelegate1;


        private readonly SimpleDelegate simpleDelegate11;
        private readonly SimpleDelegate simpleDelegate2;

        private readonly int size = 1000000;

        public DelegateTests()
        {
            Array = new int[size];

            BookForOneDelegates = new Book {SimpleDelegate = Calculate};
            BookForTwoDelegates = new Book {SimpleDelegate = Calculate};
            BookForManyDelegates = new Book {SimpleDelegate = Calculate};

            BookForTwoDelegates.SimpleDelegate += Calculate1;

            BookForManyDelegates.SimpleDelegate += Calculate1;
            BookForManyDelegates.SimpleDelegate += Calculate2;
            BookForManyDelegates.SimpleDelegate += Calculate3;
            BookForManyDelegates.SimpleDelegate += Calculate4;
            BookForManyDelegates.SimpleDelegate += Calculate5;
            BookForManyDelegates.SimpleDelegate += Calculate6;


            simpleDelegate1 = Calculate;
            simpleDelegate2 = Calculate1;

            simpleDelegate11 = Calculate;
            simpleDelegate11 += Calculate1;
        }

        [Benchmark]
        public void CallMethod()
        {
            Calculate();
        }

        [Benchmark]
        public void CallTwoMethod()
        {
            Calculate();
            Calculate();
        }

        [Benchmark]
        public void CallManyMethod()
        {
            Calculate();
            Calculate1();
            Calculate2();
            Calculate3();
            Calculate4();
            Calculate5();
            Calculate6();
        }

        [Benchmark]
        public void OneMethodInDelegate()
        {
            BookForOneDelegates.SimpleDelegate.Invoke();
        }

        [Benchmark]
        public void TwoMethodInDelegate()
        {
            BookForTwoDelegates.SimpleDelegate.Invoke();
        }

        [Benchmark]
        public void ManyMethodInDelegate()
        {
            BookForManyDelegates.SimpleDelegate.Invoke();
        }

        [Benchmark]
        public void OneDelegate_InvokeTwice()
        {
            SimpleDelegate simpleDelegate = Calculate;

            simpleDelegate.Invoke();
            simpleDelegate.Invoke();
        }

        [Benchmark]
        public void TwoDelegate_InvokeOnce()
        {
            simpleDelegate1.Invoke();
            simpleDelegate2.Invoke();
        }

        [Benchmark]
        public void OneDelegate_InvokeTwoMethod()
        {
            simpleDelegate11.Invoke();
        }

        public void Calculate()
        {
            var a = 1;
            var b = 2;

            var c = a + b;
        }

        public void Calculate1()
        {
            var a = 1;
            var b = 2;

            var c = a + b;
        }

        public void Calculate2()
        {
            var a = 1;
            var b = 2;

            var c = a + b;
        }

        public void Calculate3()
        {
            var a = 1;
            var b = 2;

            var c = a + b;
        }

        public void Calculate4()
        {
            var a = 1;
            var b = 2;

            var c = a + b;
        }

        public void Calculate5()
        {
            var a = 1;
            var b = 2;

            var c = a + b;
        }

        public void Calculate6()
        {
            var a = 1;
            var b = 2;

            var c = a + b;
        }
    }
}