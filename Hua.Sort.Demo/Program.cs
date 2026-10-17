using System.Diagnostics;

namespace Hua.Sort.Demo;

internal class Program
{
    static void Main(string[] args)
    {
        int[] numbers = GenerateRandomArray(10);
        Console.WriteLine("Unsorted: " + string.Join(", ", numbers));

        var stopwatch = Stopwatch.StartNew();

        var sorter = new BubbleSort<int>();
        sorter.Sort(numbers);

        Console.WriteLine("Sorted: " + string.Join(", ", numbers));
        Console.WriteLine($"Time: {stopwatch.Elapsed.TotalMilliseconds:F5}ms");
    }

    static int[] GenerateRandomArray(int length)
    {
        var array = Enumerable.Range(0, length).ToArray();
        var random = new Random();

        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }

        return array;
    }
}
