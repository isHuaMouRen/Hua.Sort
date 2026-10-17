using System.Numerics;

namespace Hua.Sort;

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
