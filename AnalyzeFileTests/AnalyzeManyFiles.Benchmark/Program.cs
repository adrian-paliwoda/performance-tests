using BenchmarkDotNet.Running;

// var summary = BenchmarkRunner.Run<AnalyzeManyFiles.Benchmark.Benchmarks.AnalyzerLargeFiles>();
var summary = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

// var test = new AnalyzerFile();
// test.SmallFile_StreamReaderWithReadLineStrategy();