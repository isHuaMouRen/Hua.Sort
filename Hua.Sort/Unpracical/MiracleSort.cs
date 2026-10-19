using System.Numerics;

namespace Hua.Sort.Unpracical;

[Obsolete("MiracleSort can never be completed under normal circumstances and is only suitable for entertainment purposes. Do not use it in a production environment.")]
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
