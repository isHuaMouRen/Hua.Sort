using System.Numerics;

namespace Hua.Sort;

/// <summary>
/// https://zh.wikipedia.org/wiki/%E4%BE%8F%E5%84%92%E6%8E%92%E5%BA%8F
/// </summary>
/// <typeparam name="T"></typeparam>
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

