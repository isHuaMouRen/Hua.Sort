using System.Numerics;

namespace Hua.Sort;

/// <summary>
/// https://zh.wikipedia.org/wiki/%E8%AE%A1%E6%95%B0%E6%8E%92%E5%BA%8F
/// </summary>
public class CountingSort<T> : ISort<T> where T : INumber<T>
{
    public void Sort(T[] array)
    {
        if (array.Length <= 1)
            return;

        int min = int.CreateChecked(array[0]);
        int max = min;

        foreach (T value in array)
        {
            int number = int.CreateChecked(value);

            if (number < min) min = number;
            if (number > max) max = number;
        }

        int range = checked(max - min + 1);
        var counts = new int[range];

        foreach (T value in array)
        {
            counts[int.CreateChecked(value) - min]++;
        }

        int index = 0;

        for (int i = 0; i < counts.Length; i++)
        {
            while (counts[i]-- > 0)
            {
                array[index++] = T.CreateChecked(i + min);
            }
        }
    }
}
