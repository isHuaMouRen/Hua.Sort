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

        var buckets = new List<T>?[BucketCount];

        foreach (T value in array)
        {
            double current = double.CreateChecked(value);
            double ratio = (current - minValue) / range;

            int index = (int)(ratio * BucketCount);

            if (index < 0) index = 0;
            if (index >= BucketCount) index = BucketCount - 1;

            var bucket = buckets[index];
            if (bucket is null)
            {
                bucket = [];
                buckets[index] = bucket;
            }

            bucket.Add(value);
        }

        int outputIndex = 0;
        foreach (var bucket in buckets)
        {
            if (bucket is null)
                continue;

            for (int i = 1; i < bucket.Count; i++)
            {
                T value = bucket[i];
                int j = i - 1;

                while (j >= 0 && bucket[j] > value)
                {
                    bucket[j + 1] = bucket[j];
                    j--;
                }

                bucket[j + 1] = value;
            }

            foreach (T value in bucket)
                array[outputIndex++] = value;
        }
    }
}
