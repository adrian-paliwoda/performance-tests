using AnalyzeFile.Benchmark.Benchmarks;
using BenchmarkDotNet.Running;

// var summary = BenchmarkRunner.Run<AnalyzeFile.Benchmark.Benchmarks.AnalyzerFile>();
var summary = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

// var test = new AnalyzerFile();
// test.SmallFile_StreamReaderWithReadLineStrategy();