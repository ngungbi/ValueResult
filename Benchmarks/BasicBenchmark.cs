using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Ngb.ValueResult;

namespace Benchmarks;


[IterationCount(1)]
// [SimpleJob(RunStrategy.Monitoring, 10, 10, 10, 50)]
[MemoryDiagnoser]
public class BasicBenchmark {
    [Benchmark]
    public Result<int> CreateResultOfInteger() {
        var result = 10;
        return result;
    }

    [Benchmark]
    public Result<int> CreateResultOfError() {
        var result = new Error(400, string.Empty, string.Empty);
        return result;
    }
}
