using System.Numerics;

namespace Hua.Sort;

/// <summary>
/// https://zh.wikipedia.org/wiki/%E9%B8%A1%E5%B0%BE%E9%85%92%E6%8E%92%E5%BA%8F
/// </summary>
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
