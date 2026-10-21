using System.Numerics;

namespace Hua.Sort;

public static class SortExtensions
{
    public static T[] SortCopy<T>(this ISort<T> sorter, T[] array) where T : INumber<T>
    {
        ArgumentNullException.ThrowIfNull(sorter);
        ArgumentNullException.ThrowIfNull(array);

        T[] copy = (T[])array.Clone();
        sorter.Sort(copy);
        return copy;
    }
}
