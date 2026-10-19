using System.Diagnostics;
using Hua.Sort;

namespace Hua.Sort.Test
{
    internal class Program
    {
        static int[] GenerateRandomArray(int length)
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
            var numbers = GenerateRandomArray(10);
            Console.WriteLine($"Unsorted: {string.Join(", ", numbers)}\n");

            int[][] testCases =
            [
                numbers,
                [],
                [42],
                [3, -1, 3, 0, -1, 2, 2],
                [-5, -4, -3, -2, -1, 0, 1, 2, 3, 4],
                [4, 3, 2, 1, 0, -1, -2, -3, -4, -5]
            ];

            foreach (var type in GetSorterTypes())
            {
                var sorter = CreateSorter(type);
                foreach (var testCase in testCases)
                {
                    var expected = (int[])testCase.Clone();
                    Array.Sort(expected);
                    var copy = (int[])testCase.Clone();

                    var stopwatch = Stopwatch.StartNew();
                    sorter.Sort(copy);
                    stopwatch.Stop();

                    if (!copy.SequenceEqual(expected))
                        throw new InvalidOperationException($"{type.Name} produced an incorrectly sorted array.");

                    if (ReferenceEquals(testCase, numbers))
                    {
                        var sorterName = type.IsGenericType ? type.GetGenericTypeDefinition().Name : type.Name;
                        sorterName = sorterName.Split('`')[0];
                        Console.WriteLine($"{sorterName}[{stopwatch.Elapsed.TotalMilliseconds:F3}ms] {string.Join(", ", copy)}");
                    }
                }
            }
        }
    }
}