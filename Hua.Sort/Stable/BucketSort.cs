using System.Numerics;

namespace Hua.Sort.Stable;

public class BucketSort<T> : ISort<T> where T : INumber<T>
{
    public int BucketCount { get; }

    public BucketSort(int bucketCount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bucketCount);
        BucketCount = bucketCount;
    }

    private class ListNode(T data)
    {
        public T Data = data;
        public ListNode? Next;
    }

    private static ListNode? Insert(ListNode? head, T value)
    {
        var dummy = new ListNode(value) { Next = head };
        var prev = dummy;

        while (prev.Next is not null &&
               prev.Next.Data <= value)
        {
            prev = prev.Next;
        }

        prev.Next = new ListNode(value) { Next = prev.Next };

        return dummy.Next;
    }

    private static ListNode? Merge(ListNode? head1, ListNode? head2)
    {
        var dummy = new ListNode(T.Zero);
        var tail = dummy;

        while (head1 is not null && head2 is not null)
        {
            if (head1.Data <= head2.Data)
            {
                tail.Next = head1;
                head1 = head1.Next;
            }
            else
            {
                tail.Next = head2;
                head2 = head2.Next;
            }

            tail = tail.Next;
        }

        tail.Next = head1 ?? head2;

        return dummy.Next;
    }

    public void Sort(T[] array)
    {
        ArgumentNullException.ThrowIfNull(array);

        if (array.Length <= 1)
            return;

        T min = array[0];
        T max = array[0];

        foreach (T value in array)
        {
            if (!T.IsFinite(value))
                throw new ArgumentException(
                    "BucketSort does not support NaN or infinity values.",
                    nameof(array));

            if (value < min) min = value;
            if (value > max) max = value;
        }

        if (min == max)
            return;

        double minValue = double.CreateChecked(min);
        double maxValue = double.CreateChecked(max);
        double range = maxValue - minValue;

        if (!double.IsFinite(range) || range <= 0)
            throw new ArgumentException(
                "The value range cannot be represented reliably as double.",
                nameof(array));

        var buckets = new ListNode?[BucketCount];

        foreach (T value in array)
        {
            double current = double.CreateChecked(value);
            double ratio = (current - minValue) / range;

            int index = (int)(ratio * BucketCount);

            if (index < 0) index = 0;
            if (index >= BucketCount) index = BucketCount - 1;

            buckets[index] = Insert(buckets[index], value);
        }

        ListNode? head = null;

        foreach (var bucket in buckets)
        {
            head = Merge(head, bucket);
        }

        int i = 0;

        while (head is not null)
        {
            array[i++] = head.Data;
            head = head.Next;
        }
    }
}
