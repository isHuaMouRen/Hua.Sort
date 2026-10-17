using System.Numerics;

namespace Hua.Sort;

/// <summary>
/// https://zh.wikipedia.org/wiki/%E9%80%89%E6%8B%A9%E6%8E%92%E5%BA%8F
/// </summary>
public class SelectionSort<T> : ISort<T> where T : INumber<T>
{
    public void Sort(T[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int min = i;

            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[j] < array[min])
                    min = j;
            }

            if (min != i)
                (array[i], array[min]) = (array[min], array[i]);
        }
    }
}
