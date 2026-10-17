using System.Numerics;

namespace Hua.Sort;

public class ShellSort<T> : ISort<T> where T : INumber<T>
{
    public void Sort(T[] array)
    {
        int h = array.Length / 2;
        while (h >= 1)
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = i + h; j < array.Length; j += h)
                {
                    T temp = array[j];
                    int loc = j;
                    while (loc - h >= i && temp < array[loc - h])
                    {
                        array[loc] = array[loc - h];
                        loc = loc - h;
                    }
                    array[loc] = temp;
                }
            }
            h = h / 2;
        }
    }
}
