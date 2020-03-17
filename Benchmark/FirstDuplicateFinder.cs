using BenchmarkDotNet.Attributes;

namespace Benchmark
{
    [MemoryDiagnoser]
    [RyuJitX64Job]
    public class FirstDuplicateFinder
    {
        const int N = 1_000_000_000;
        private readonly int[] _ints = new int[N];

        [IterationSetup]
        public void Setup()
        {
            for (int i = 0; i < N; i++)
            {
                _ints[i] = N - i;
            }
        }

        [Benchmark]
        public void NoDuplicate()
        {
            int duplicate = FindFirstDuplicate(_ints);
        }

        [Benchmark]
        public void DuplicateInTheMiddle()
        {
            _ints[N / 2] = N;
            int duplicate = FindFirstDuplicate(_ints);
        }

        [Benchmark]
        public void NoDuplicateWithDictionary()
        {
            int duplicate = FindFirstDuplicateWithDictionary(_ints);
        }

        [Benchmark]
        public void DuplicateInTheMiddleWithDictionary()
        {
            _ints[N / 2] = N;
            int duplicate = FindFirstDuplicateWithDictionary(_ints);
        }

        private int FindFirstDuplicate(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                var number = numbers[i];
                if (number < 0)
                {
                    number = -number;
                }
                if (numbers[number - 1] < 0) // Found a duplicate
                {
                    return number;
                }
                numbers[number - 1] = -numbers[number - 1]; // Mark that this number has appeared once
            }
            return -1;
        }

        private int FindFirstDuplicateWithDictionary(int[] numbers)
        {
            var dict = new bool[numbers.Length + 1];
            for (int i = 0; i < numbers.Length; i++)
            {
                var number = numbers[i];
                if (dict[number]) // Found a duplicate
                {
                    return number;
                }
                dict[number] = true; // Mark that this number has appeared once
            }

            return -1;
        }
    }
}