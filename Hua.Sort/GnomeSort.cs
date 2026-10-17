using System.Numerics;

namespace Hua.Sort;

public class GnomeSort<T> : ISort<T> where T : INumber<T>
{
    public void Sort(T[] array)
    {
        int pos = 1;

        while (pos < array.Length)
        {
            if (pos == 0 || array[pos] >= array[pos - 1])
            {
                pos++;
            }
            else
            {
                (array[pos], array[pos - 1]) = (array[pos - 1], array[pos]);
                pos--;
            }
        }
    }
}

