using System.Numerics;

namespace Hua.Sort.Impractical;

[Obsolete("This sorting algorithm will never complete the sorting process under normal circumstances. Do not use it in production environments. It is only for entertainment purposes.")]
public class MiracleSort<T> : ISort<T> where T : INumber<T>
{
    public void Sort(T[] array)
    {
        while (true)
        {
            if (IsSorted(array))
                break;
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
