using System.Numerics;

namespace Hua.Sort.Unstable;

public class IntroSort<T> : ISort<T> where T : INumber<T>
{
    private const int InsertionSortThreshold = 16;

    public void Sort(T[] array)
    {
        if (array.Length < 2)
            return;

        int depthLimit = 0;
        for (int length = array.Length; length > 1; length >>= 1)
            depthLimit += 2;

        IntroSortRange(array, 0, array.Length - 1, depthLimit);
    }

    private static void IntroSortRange(T[] array, int low, int high, int depthLimit)
    {
        while (high - low > InsertionSortThreshold)
        {
            if (depthLimit == 0)
            {
                HeapSort(array, low, high);
                return;
            }

            depthLimit--;
            int middle = low + (high - low) / 2;
            T pivot = MedianOfThree(array[low], array[middle], array[high]);
            int left = low;
            int right = high;

            while (left <= right)
            {
                while (array[left] < pivot)
                    left++;

                while (array[right] > pivot)
                    right--;

                if (left <= right)
                {
                    (array[left], array[right]) = (array[right], array[left]);
                    left++;
                    right--;
                }
            }

            if (right - low < high - left)
            {
                if (low < right)
                    IntroSortRange(array, low, right, depthLimit);

                low = left;
            }
            else
            {
                if (left < high)
                    IntroSortRange(array, left, high, depthLimit);

                high = right;
            }
        }

        InsertionSort(array, low, high);
    }

    private static T MedianOfThree(T first, T second, T third)
    {
        if (first > second)
            (first, second) = (second, first);

        if (second > third)
            (second, third) = (third, second);

        if (first > second)
            (first, second) = (second, first);

        return second;
    }

    private static void InsertionSort(T[] array, int low, int high)
    {
        for (int i = low + 1; i <= high; i++)
        {
            T value = array[i];
            int j = i - 1;

            while (j >= low && array[j] > value)
            {
                array[j + 1] = array[j];
                j--;
            }

            array[j + 1] = value;
        }
    }

    private static void HeapSort(T[] array, int low, int high)
    {
        int length = high - low + 1;

        for (int root = length / 2 - 1; root >= 0; root--)
            SiftDown(array, low, root, length);

        for (int end = length - 1; end > 0; end--)
        {
            (array[low], array[low + end]) = (array[low + end], array[low]);
            SiftDown(array, low, 0, end);
        }
    }

    private static void SiftDown(T[] array, int offset, int root, int length)
    {
        while (root * 2 + 1 < length)
        {
            int child = root * 2 + 1;
            if (child + 1 < length && array[offset + child] < array[offset + child + 1])
                child++;

            if (array[offset + root] >= array[offset + child])
                return;

            (array[offset + root], array[offset + child]) =
                (array[offset + child], array[offset + root]);
            root = child;
        }
    }
}
