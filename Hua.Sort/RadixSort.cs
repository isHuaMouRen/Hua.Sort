namespace Hua.Sort;

/// <summary>
/// https://zh.wikipedia.org/wiki/%E5%9F%BA%E6%95%B0%E6%8E%92%E5%BA%8F
/// </summary>
public class RadixSort : ISort<int>
{
    public void Sort(int[] array)
    {
        if (array.Length < 2)
            return;

        int[] buffer = new int[array.Length];
        int[] count = new int[256];

        for (int shift = 0; shift < 32; shift += 8)
        {
            Array.Clear(count);

            foreach (int value in array)
            {
                uint key = unchecked((uint)value) ^ 0x80000000u;
                count[(int)((key >> shift) & 0xFF)]++;
            }

            for (int i = 1; i < count.Length; i++)
                count[i] += count[i - 1];

            for (int i = array.Length - 1; i >= 0; i--)
            {
                uint key = unchecked((uint)array[i]) ^ 0x80000000u;
                int digit = (int)((key >> shift) & 0xFF);

                buffer[--count[digit]] = array[i];
            }

            (array, buffer) = (buffer, array);
        }
    }
}
