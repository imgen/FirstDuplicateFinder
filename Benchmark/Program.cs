using BenchmarkDotNet.Running;
using System;

namespace Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<FirstDuplicateFinder>();
            Console.ReadKey();
        }
    }
}