using System.Numerics;

namespace Hua.Sort.Unstable;

public class QuickSort<T> : ISort<T> where T : INumber<T>
{
    public void Sort(T[] array)
    {
        QuickSortInternal(array, 0, array.Length - 1);
    }

    private static void QuickSortInternal(T[] array, int low, int high)
    {
        if (low >= high)
            return;

        T pivot = array[low + (high - low) / 2];
        int i = low, j = high;

        while (i <= j)
        {
            while (array[i] < pivot)
                i++;

            while (array[j] > pivot)
                j--;

            if (i <= j)
            {
                (array[i], array[j]) = (array[j], array[i]);
                i++;
                j--;
            }
        }

        if (low < j)
            QuickSortInternal(array, low, j);

        if (i < high)
            QuickSortInternal(array, i, high);
    }
}
