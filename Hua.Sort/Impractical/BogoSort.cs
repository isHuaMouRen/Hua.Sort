using System.Numerics;

namespace Hua.Sort.Impractical;

[Obsolete("This sorting algorithm will never complete the sorting process under normal circumstances. Do not use it in production environments. It is only for entertainment purposes.")]
public class BogoSort<T> : ISort<T> where T : INumber<T>
{
    public void Sort(T[] array)
    {
        var random = new Random();

        while (!IsSorted(array))
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
        }
    }

    private static bool IsSorted(T[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i - 1] > array[i])
                return false;
        }

        return true;
    }
}
