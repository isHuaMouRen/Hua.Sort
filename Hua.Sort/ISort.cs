using System.Numerics;

namespace Hua.Sort;

public interface ISort<T> where T : INumber<T>
{
    void Sort(T[] array);
}