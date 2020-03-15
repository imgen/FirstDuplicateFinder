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
        public void NoDuplicateWithRestoration()
        {
            int duplicate = FindFirstDuplicateWithRestoration(_ints);
        }
        
        [Benchmark]
        public void DuplicateInTheMiddleWithRestoration()
        {
            _ints[N / 2] = N;
            int duplicate = FindFirstDuplicateWithRestoration(_ints);
        }
        
        private int FindFirstDuplicate(int[] numbers)
        {
            for(int i = 0; i < numbers.Length; i++)
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
        
        private int FindFirstDuplicateWithRestoration(int[] numbers)
        {
            var firstDuplicate = -1;
            for(int i = 0; i < numbers.Length; i++)
            {
                var number = numbers[i];
                if (number < 0)
                {
                    number = -number;
                }
                if (numbers[number - 1] < 0) // Found a duplicate
                {
                    firstDuplicate = number;
                    break;
                }
                numbers[number - 1] = -numbers[number - 1]; // Mark that this number has appeared once
            }
            
            // Restore data
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] < 0)
                {
                    numbers[i] = -numbers[i];
                }
            }
            return firstDuplicate;
        }
    }
}