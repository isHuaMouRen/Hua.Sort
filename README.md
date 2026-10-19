# Hua. Sort

**Hua. Sort** 是一个使用 C# 实现的排序算法集合，为多种经典排序算法提供统一的封装接口。

## 使用示例

### 自行编写代码

引用排序算法所在的命名空间，创建对应排序算法的实例，并调用 `Sort()` 方法即可。

```csharp
using Hua.Sort.Stable;

static void Main(string[] args)
{
    int[] numbers = { 7, 5, 3, 9, 1, 4, 6, 8, 2, 0 };

    var sorter = new BubbleSort<int>();
    sorter.Sort(numbers);

    Console.WriteLine(string.Join(", ", numbers));
}
```

输出：

```text
0, 1, 2, 3, 4, 5, 6, 7, 8, 9
```

上述示例使用 `BubbleSort<int>` 进行冒泡排序。你可以将 `BubbleSort` 替换为其他排序算法对应的类名，以使用不同的排序算法。

若算法支持，可以将 `<int>` 替换为其他支持的类型 *( `double` , `float` , `long` ...)* 来进行排序，具体哪些算法支持请看下方列表

### 查看 Demo

克隆本仓库，使用 **Visual Studio** 打开解决方案，然后进入 `Hua.Sort.Demo` 项目，即可查看示例代码、编译项目并运行测试。

## 算法列表

> 稳定的算法在命名空间 `Hua.Sort.Stable` 下，不稳定的则在 `Hua.Sort.Unstable`

|算法|类名|稳定?|支持的类型|
|-|-|-|-|
|冒泡排序| `BubbleSort` | `Yes` | `INumber<T>` |
|桶排序| `BucketSort` | `Yes` | `INumber<T>` |
|鸡尾酒排序| `CocktailShakerSort` | `Yes` | `INumber<T>` |
|计数排序| `CountingSort` | `Yes` | `int` |
|侏儒排序| `GnomeSort` | `Yes` | `INumber<T>` |
|插入排序| `InsertionSort` | `Yes` | `INumber<T>` |
|归并排序| `MergeSort` | `Yes` | `INumber<T>` |
|鸽巢排序| `PigeonholeSort` | `Yes` | `int` |
|基数排序| `RadixSort` | `Yes` | `int` |
|梳排序| `CombSort` | `No` | `INumber<T>` |
|堆排序| `HeapSort` | `No` | `INumber<T>` |
|快速排序| `QuickSort` | `No` | `INumber<T>` |
|选择排序| `SelectionSort` | `No` | `INumber<T>` |
|希尔排序| `ShellSort` | `No` | `INumber<T>` |

### 不实用的排序

> 命名空间: `Hua.Sort.Unpracical`

|算法|类名|支持的类型|
|-|-|-|
|猴子排序| `BogoSort` | `INumber<T>` |
