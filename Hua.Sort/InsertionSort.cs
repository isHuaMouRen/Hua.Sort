using System.Numerics;

namespace Hua.Sort;

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
