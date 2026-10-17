using System.Numerics;

namespace Hua.Sort;

public class CocktailShakerSort<T> : ISort<T> where T : INumber<T>
{
    public void Sort(T[] array)
    {
        int i, left = 0, right = array.Length - 1;
        T temp;
        while (left < right)
        {
            for (i = left; i < right; i++)
            {
                if (array[i] > array[i + 1])
                {
                    temp = array[i];
                    array[i] = array[i + 1];
                    array[i + 1] = temp;
                }
            }
            right--;
            for (i = right; i > left; i--)
            {
                if (array[i - 1] > array[i])
                {
                    temp = array[i];
                    array[i] = array[i - 1];
                    array[i - 1] = temp;
                }
            }
            left++;
        }
    }
}
