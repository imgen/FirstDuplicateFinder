using BenchmarkDotNet.Running;

namespace Benchmark
{
    public class Program
    {
        public static void Main(string[] args) => 
            BenchmarkRunner
                .Run<FirstDuplicateFinder>();
    }
}