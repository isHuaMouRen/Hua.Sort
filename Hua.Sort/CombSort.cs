using System.Numerics;

namespace Hua.Sort;

/// <summary>
/// https://zh.wikipedia.org/wiki/%E6%A2%B3%E6%8E%92%E5%BA%8F
/// </summary>
public class CombSort<T> : ISort<T> where T : INumber<T>
{
    public void Sort(T[] array)
    {
        double shrinkFactor = 0.8;
        int gap = array.Length;
        bool swapped = true;

        while (gap > 1 || swapped)
        {
            if (gap > 1)
                gap = (int)(gap * shrinkFactor);

            swapped = false;

            for (int i = 0; gap + i < array.Length; i++)
            {
                if (array[i] > array[i + gap])
                {
                    (array[i], array[i + gap]) = (array[i + gap], array[i]);
                    swapped = true;
                }
            }
        }
    }
}
