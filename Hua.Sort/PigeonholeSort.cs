using System.Numerics;

namespace Hua.Sort;

/// <summary>
/// https://zh.wikipedia.org/wiki/%E9%B8%BD%E5%B7%A2%E6%8E%92%E5%BA%8F
/// </summary>
/// <typeparam name="T"></typeparam>
public class PigeonholeSort<T> : ISort<T> where T : INumber<T>
{
    public void Sort(T[] array)
    {
        if (array.Length <= 1)
            return;

        T min = array[0];
        T max = array[0];

        foreach (T value in array)
        {
            if (value < min) min = value;
            if (value > max) max = value;
        }

        int minValue = int.CreateChecked(min);
        int maxValue = int.CreateChecked(max);
        int range = checked(maxValue - minValue + 1);

        var holes = new int[range];

        foreach (T value in array)
        {
            holes[int.CreateChecked(value) - minValue]++;
        }

        int index = 0;

        for (int i = 0; i < holes.Length; i++)
        {
            while (holes[i]-- > 0)
            {
                array[index++] = T.CreateChecked(i + minValue);
            }
        }
    }
}
