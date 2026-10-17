using System.Numerics;

namespace Hua.Sort;

public class BubbleSort<T> : ISort<T> where T : INumber<T>
{
    public void Sort(T[] array)
    {
        T temp;

        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - 1 - i; j++)
            {
                if (array[j] > array[j + 1])
                {
                    temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }
}
