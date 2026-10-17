using System.Numerics;

namespace Hua.Sort;

public class HeapSort<T> : ISort<T> where T : INumber<T>
{
    public void Sort(T[] array)
    {
        int len = array.Length;

        for (int i = len / 2 - 1; i >= 0; i--)
            MaxHeapify(array, i, len - 1);

        for (int i = len - 1; i > 0; i--)
        {
            (array[0], array[i]) = (array[i], array[0]);
            MaxHeapify(array, 0, i - 1);
        }
    }

    private static void MaxHeapify(T[] array, int start, int end)
    {
        int dad = start;
        int son = dad * 2 + 1;

        while (son <= end)
        {
            if (son + 1 <= end && array[son] < array[son + 1])
                son++;

            if (array[dad] > array[son])
                return;

            (array[dad], array[son]) = (array[son], array[dad]);

            dad = son;
            son = dad * 2 + 1;
        }
    }
}
