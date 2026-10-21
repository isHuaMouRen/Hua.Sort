using System.Diagnostics;
using Hua.Sort;

namespace Hua.Sort.Test
{
    internal class Program
    {
        static int[] GenerateRandomUniqueArray(int length)
        {
            var array = Enumerable.Range(-length / 2, length).ToArray();
            var random = new Random();

            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }

            return array;
        }

        static int[] GenerateRandomRepeatedArray(int length)
        {
            var random = new Random();
            return Enumerable.Range(0, length)
                .Select(_ => random.Next(10))
                .ToArray();
        }

        static IEnumerable<Type> GetSorterTypes()
        {
            return typeof(ISort<>).Assembly
                .GetTypes()
                .Where(type =>
                    type.IsClass &&
                    !type.IsAbstract &&
                    type.Namespace is "Hua.Sort.Stable" or "Hua.Sort.Unstable")
                .Select(type => type.IsGenericTypeDefinition
                    ? type.MakeGenericType(typeof(int))
                    : type)
                .Where(type => type.GetInterfaces().Any(interfaceType =>
                    interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(ISort<>) &&
                    interfaceType.GetGenericArguments()[0] == typeof(int)))
                .OrderBy(type => type.Name, StringComparer.Ordinal);
        }

        static ISort<int> CreateSorter(Type type)
        {
            var parameterlessConstructor = type.GetConstructor(Type.EmptyTypes);
            if (parameterlessConstructor is not null)
                return (ISort<int>)parameterlessConstructor.Invoke(null);

            var intConstructor = type.GetConstructor([typeof(int)]);
            if (intConstructor is not null)
                return (ISort<int>)intConstructor.Invoke([10]);

            throw new InvalidOperationException($"No supported constructor found for {type.FullName}.");
        }



        static void Main(string[] args)
        {
            const int arrayLength = 1000;//<---可修改
            const int timingRuns = 1000;//<---可修改
            var randomUniqueArray = GenerateRandomUniqueArray(arrayLength);

            int[][] nonTimingTestCases =
            [
                GenerateRandomRepeatedArray(arrayLength),
                Enumerable.Range(1, arrayLength).Reverse().ToArray(),
                Enumerable.Range(1, arrayLength).ToArray(),
                [],
                [42]
            ];

            var totalStopwatch = Stopwatch.StartNew();

            foreach (var type in GetSorterTypes())
            {
                var sorter = CreateSorter(type);
                // 可行性测试
                foreach (var testCase in nonTimingTestCases)
                {
                    var copy = (int[])testCase.Clone();
                    var expectedCase = (int[])testCase.Clone();
                    Array.Sort(expectedCase);
                    sorter.Sort(copy);

                    if (!copy.SequenceEqual(expectedCase))
                        throw new InvalidOperationException($"{type.Name} failed a non-timing test case.");
                }

                //性能测试
                var expected = (int[])randomUniqueArray.Clone();
                Array.Sort(expected);
                var elapsedTicks = new long[timingRuns];
                for (var i = 0; i < timingRuns; i++)
                {
                    var copy = (int[])randomUniqueArray.Clone();
                    var stopwatch = Stopwatch.StartNew();
                    sorter.Sort(copy);
                    stopwatch.Stop();
                    elapsedTicks[i] = stopwatch.ElapsedTicks;

                    if (!copy.SequenceEqual(expected))
                        throw new InvalidOperationException($"{type.Name} produced an incorrectly sorted array.");
                }

                Array.Sort(elapsedTicks);
                var medianTicks = (elapsedTicks[timingRuns / 2 - 1] + elapsedTicks[timingRuns / 2]) / 2d;
                var medianMilliseconds = medianTicks * 1000d / Stopwatch.Frequency;
                var sorterName = type.IsGenericType ? type.GetGenericTypeDefinition().Name : type.Name;
                sorterName = sorterName.Split('`')[0];
                Console.WriteLine($"{sorterName}[{medianMilliseconds:F3}ms]");
            }

            totalStopwatch.Stop();
            Console.WriteLine($"\n[Test Completed] Total time taken: {totalStopwatch.Elapsed.TotalMilliseconds:F3}ms");
        }
    }
}