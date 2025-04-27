using BenchmarkDotNet.Running;
using PerformanceTests.Tests;

namespace PerformanceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            // var summary1 = BenchmarkRunner.Run<CollectionsObjectsTests>();
            // var summary2 = BenchmarkRunner.Run<CollectionsTests>();
            // var summary3 = BenchmarkRunner.Run<CountTests>();
            // var summary4 = BenchmarkRunner.Run<DelegateTests>();
            // var summary6 = BenchmarkRunner.Run<ForArrayTests>();
            // var summary7 = BenchmarkRunner.Run<ForEnumerableTests>();
            // var summary8 = BenchmarkRunner.Run<ForVsForeach>();

            // var summary12 = BenchmarkRunner.Run<StringConcat>();

            var summary11 = BenchmarkRunner.Run<PointerTests>();
            // var summary10 = BenchmarkRunner.Run<MultiDimensionArraysTests>();
            //var summary9 = BenchmarkRunner.Run<GarbageCollectionTests>();
            // var summary5 = BenchmarkRunner.Run<ExceptionTests>();
            // var summary = BenchmarkRunner.Run<CatchExceptionTests>();

        }
    }
}