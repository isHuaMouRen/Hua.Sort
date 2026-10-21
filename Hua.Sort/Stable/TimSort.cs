using System.Numerics;

namespace Hua.Sort.Stable;

public class TimSort<T> : ISort<T> where T : INumber<T>
{
    private const int MinMerge = 32;
    private const int MaxRunStackSize = 85;

    public void Sort(T[] array)
    {
        int length = array.Length;
        if (length < 2)
            return;

        int minRun = GetMinRunLength(length);
        T[] temp = new T[length / 2];
        int[] runBases = new int[MaxRunStackSize];
        int[] runLengths = new int[MaxRunStackSize];
        int stackSize = 0;
        int current = 0;

        while (current < length)
        {
            int runLength = CountRunAndMakeAscending(array, current, length);
            int forcedRunLength = Math.Min(minRun, length - current);

            if (runLength < forcedRunLength)
            {
                BinaryInsertionSort(array, current, current + forcedRunLength, current + runLength);
                runLength = forcedRunLength;
            }

            runBases[stackSize] = current;
            runLengths[stackSize] = runLength;
            stackSize++;

            MergeCollapse(array, temp, runBases, runLengths, ref stackSize);
            current += runLength;
        }

        MergeForceCollapse(array, temp, runBases, runLengths, ref stackSize);
    }

    private static int GetMinRunLength(int length)
    {
        int remainder = 0;
        while (length >= MinMerge)
        {
            remainder |= length & 1;
            length >>= 1;
        }

        return length + remainder;
    }

    private static int CountRunAndMakeAscending(T[] array, int start, int end)
    {
        int runEnd = start + 1;
        if (runEnd == end)
            return 1;

        if (array[runEnd] < array[start])
        {
            while (++runEnd < end && array[runEnd] < array[runEnd - 1])
            {
            }

            Array.Reverse(array, start, runEnd - start);
        }
        else
        {
            while (++runEnd < end && array[runEnd] >= array[runEnd - 1])
            {
            }
        }

        return runEnd - start;
    }

    private static void BinaryInsertionSort(T[] array, int start, int end, int sortedEnd)
    {
        if (start == sortedEnd)
            sortedEnd++;

        for (; sortedEnd < end; sortedEnd++)
        {
            T pivot = array[sortedEnd];
            int left = start;
            int right = sortedEnd;

            while (left < right)
            {
                int middle = left + ((right - left) >> 1);
                if (pivot < array[middle])
                    right = middle;
                else
                    left = middle + 1;
            }

            for (int index = sortedEnd; index > left; index--)
                array[index] = array[index - 1];

            array[left] = pivot;
        }
    }

    private static void MergeCollapse(T[] array, T[] temp, int[] runBases, int[] runLengths, ref int stackSize)
    {
        while (stackSize > 1)
        {
            int index = stackSize - 2;
            if ((index > 0 && runLengths[index - 1] <= runLengths[index] + runLengths[index + 1]) ||
                (index > 1 && runLengths[index - 2] <= runLengths[index - 1] + runLengths[index]))
            {
                if (runLengths[index - 1] < runLengths[index + 1])
                    index--;
            }
            else if (runLengths[index] > runLengths[index + 1])
            {
                break;
            }

            MergeAt(array, temp, runBases, runLengths, ref stackSize, index);
        }
    }

    private static void MergeForceCollapse(T[] array, T[] temp, int[] runBases, int[] runLengths, ref int stackSize)
    {
        while (stackSize > 1)
        {
            int index = stackSize - 2;
            if (index > 0 && runLengths[index - 1] < runLengths[index + 1])
                index--;

            MergeAt(array, temp, runBases, runLengths, ref stackSize, index);
        }
    }

    private static void MergeAt(T[] array, T[] temp, int[] runBases, int[] runLengths, ref int stackSize, int index)
    {
        int base1 = runBases[index];
        int length1 = runLengths[index];
        int base2 = runBases[index + 1];
        int length2 = runLengths[index + 1];

        runLengths[index] = length1 + length2;
        if (index == stackSize - 3)
        {
            runBases[index + 1] = runBases[index + 2];
            runLengths[index + 1] = runLengths[index + 2];
        }

        stackSize--;

        if (length1 <= length2)
            MergeLow(array, temp, base1, length1, base2, length2);
        else
            MergeHigh(array, temp, base1, length1, base2, length2);
    }

    private static void MergeLow(T[] array, T[] temp, int base1, int length1, int base2, int length2)
    {
        Array.Copy(array, base1, temp, 0, length1);

        int left = 0;
        int right = base2;
        int destination = base1;
        int rightEnd = base2 + length2;

        while (left < length1 && right < rightEnd)
        {
            if (temp[left] <= array[right])
                array[destination++] = temp[left++];
            else
                array[destination++] = array[right++];
        }

        if (left < length1)
            Array.Copy(temp, left, array, destination, length1 - left);
    }

    private static void MergeHigh(T[] array, T[] temp, int base1, int length1, int base2, int length2)
    {
        Array.Copy(array, base2, temp, 0, length2);

        int left = base1 + length1 - 1;
        int right = length2 - 1;
        int destination = base2 + length2 - 1;

        while (left >= base1 && right >= 0)
        {
            if (array[left] > temp[right])
                array[destination--] = array[left--];
            else
                array[destination--] = temp[right--];
        }

        if (right >= 0)
            Array.Copy(temp, 0, array, base1, right + 1);
    }
}
