using AnalyzeFile.Benchmark.Benchmarks;
using BenchmarkDotNet.Running;

// var summary = BenchmarkRunner.Run<AnalyzeFile.Benchmark.Benchmarks.AnalyzerSmallFile>();
var summary = BenchmarkRunner.Run<AnalyzeManyFiles.Benchmark.Benchmarks.AnalyzerMediumFiles>();
// var summary = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

// var test = new AnalyzerFile();
// test.SmallFile_StreamReaderWithReadLineStrateg1y();