using System.Diagnostics;
using System.Numerics;
using Hua.Sort;

namespace Hua.Sort.Test
{
    internal class Program
    {
        private static readonly Type[] NumberTypes =
        [
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(nint),
            typeof(nuint),
            typeof(Int128),
            typeof(UInt128),
            typeof(Half),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(BigInteger)
        ];

        static T[] GenerateRandomUniqueArray<T>(int length, int start) where T : INumber<T>
        {
            var array = Enumerable.Range(start, length).ToArray();
            var random = new Random();

            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }

            return array.Select(T.CreateChecked).ToArray();
        }

        static T[] GenerateRandomRepeatedArray<T>(int length) where T : INumber<T>
        {
            var random = new Random();
            return Enumerable.Range(0, length)
                .Select(_ => random.Next(10))
                .Select(T.CreateChecked)
                .ToArray();
        }

        static IEnumerable<Type> GetSorterTypes<T>() where T : INumber<T>
        {
            return typeof(ISort<>).Assembly
                .GetTypes()
                .Where(type =>
                    type.IsClass &&
                    !type.IsAbstract &&
                    type.IsGenericTypeDefinition &&
                    type.Namespace is "Hua.Sort.Stable" or "Hua.Sort.Unstable")
                .Where(type => type.GetInterfaces().Any(interfaceType =>
                    interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(ISort<>) &&
                    interfaceType.GetGenericArguments()[0] == type.GetGenericArguments()[0]))
                .Select(type => type.MakeGenericType(typeof(T)))
                .OrderBy(type => type.Name, StringComparer.Ordinal);
        }

        static ISort<T> CreateSorter<T>(Type type) where T : INumber<T>
        {
            var parameterlessConstructor = type.GetConstructor(Type.EmptyTypes);
            if (parameterlessConstructor is not null)
                return (ISort<T>)parameterlessConstructor.Invoke(null);

            var intConstructor = type.GetConstructor([typeof(int)]);
            if (intConstructor is not null)
                return (ISort<T>)intConstructor.Invoke([10]);

            throw new InvalidOperationException($"No supported constructor found for {type.FullName}.");
        }

        static void RunCorrectnessTests<T>() where T : INumber<T>
        {
            const int testArrayLength = 64;
            var uniqueArrayStart = T.IsNegative(T.CreateSaturating(-1)) ? -testArrayLength / 2 : 0;
            T[][] testCases =
            [
                GenerateRandomRepeatedArray<T>(testArrayLength),
                GenerateRandomUniqueArray<T>(testArrayLength, uniqueArrayStart),
                Enumerable.Range(1, testArrayLength).Select(T.CreateChecked).Reverse().ToArray(),
                Enumerable.Range(1, testArrayLength).Select(T.CreateChecked).ToArray(),
                Enumerable.Repeat(T.CreateChecked(7), testArrayLength).ToArray(),
                [],
                [T.CreateChecked(42)]
            ];

            foreach (var type in GetSorterTypes<T>())
            {
                var sorter = CreateSorter<T>(type);

                foreach (var testCase in testCases)
                {
                    var copy = (T[])testCase.Clone();
                    var original = (T[])testCase.Clone();
                    var expected = (T[])testCase.Clone();
                    Array.Sort(expected);

                    var sortedCopy = sorter.SortCopy(testCase);
                    if (!testCase.SequenceEqual(original) || !sortedCopy.SequenceEqual(expected))
                        throw new InvalidOperationException(
                            $"{type.Name}<{typeof(T).Name}> failed a copy-sorting test case.");

                    sorter.Sort(copy);

                    if (!copy.SequenceEqual(expected))
                        throw new InvalidOperationException(
                            $"{type.Name}<{typeof(T).Name}> failed a correctness test case.");
                }
            }
        }

        static void Main(string[] args)
        {
            const int arrayLength = 1000;//<---可修改
            const int timingRuns = 1000;//<---可修改

            var totalStopwatch = Stopwatch.StartNew();

            foreach (var numberType in NumberTypes)
            {
                if (!typeof(INumber<>).MakeGenericType(numberType).IsAssignableFrom(numberType))
                    throw new InvalidOperationException($"{numberType.Name} does not implement INumber<T>.");

                typeof(Program)
                    .GetMethod(nameof(RunCorrectnessTests), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                    .MakeGenericMethod(numberType)
                    .Invoke(null, null);

                Console.WriteLine($"[Correctness Passed] {numberType.Name}");
            }

            if (args.Contains("--correctness-only", StringComparer.Ordinal))
                return;

            var randomUniqueArray = GenerateRandomUniqueArray<int>(arrayLength, -arrayLength / 2);

            foreach (var type in GetSorterTypes<int>())
            {
                var sorter = CreateSorter<int>(type);

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
            Console.WriteLine($"\n[Test Completed] Total time taken: {totalStopwatch.Elapsed.TotalSeconds:F3}s");
        }
    }
}