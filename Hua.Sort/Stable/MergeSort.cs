using System.Numerics;

namespace Hua.Sort.Stable;

public class MergeSort<T> : ISort<T> where T : INumber<T>
{
    public void Sort(T[] array)
    {
        if (array.Length <= 1)
            return;

        T[] temp = new T[array.Length];
        Sort(array, temp, 0, array.Length - 1);
    }

    private static void Sort(T[] array, T[] temp, int left, int right)
    {
        if (left >= right)
            return;

        int mid = left + (right - left) / 2;

        Sort(array, temp, left, mid);
        Sort(array, temp, mid + 1, right);

        Merge(array, temp, left, mid, right);
    }

    private static void Merge(T[] array, T[] temp, int left, int mid, int right)
    {
        int i = left;
        int j = mid + 1;
        int k = left;

        while (i <= mid && j <= right)
        {
            if (array[i] <= array[j])
                temp[k++] = array[i++];
            else
                temp[k++] = array[j++];
        }

        while (i <= mid)
            temp[k++] = array[i++];

        while (j <= right)
            temp[k++] = array[j++];

        for (int p = left; p <= right; p++)
            array[p] = temp[p];
    }
}
