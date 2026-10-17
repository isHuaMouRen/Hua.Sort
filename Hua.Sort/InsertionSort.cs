using System.Numerics;

namespace Hua.Sort;

/// <summary>
/// https://zh.wikipedia.org/wiki/%E6%8F%92%E5%85%A5%E6%8E%92%E5%BA%8F
/// </summary>
public class InsertionSort<T>:ISort<T> where T:INumber<T>
{
    public void Sort(T[] array)
    {
        int i, j;
        T key;
        for (i = 1; i != array.Length; ++i)
        {
            key = array[i];
            j = i - 1;
            while ((j >= 0) && (array[j] > key))
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }
}
