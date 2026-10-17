# Hua.Sort

**简体中文** | [English](/README.en.md)

--- 

**Hua.Sort** 是一个使用 C# 实现的排序算法集合，为多种经典排序算法提供统一的封装接口。

## 使用示例

### 自行编写代码

引用 `Hua.Sort` 命名空间，创建对应排序算法的实例，并调用 `Sort()` 方法即可。

```csharp
using Hua.Sort;

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

若算法支持，可以将 `<int>` 替换为其他支持的类型 *( `double` , `float` , `long` ...)* 来进行排序

### 查看 Demo

克隆本仓库，使用 **Visual Studio** 打开解决方案，然后进入 `Hua.Sort.Demo` 项目，即可查看示例代码、编译项目并运行测试。

## 算法列表

|算法|类名|
|-|-|
|[冒泡排序](https://zh.wikipedia.org/wiki/冒泡排序)| `BubbleSort` |
|[桶排序](https://zh.wikipedia.org/wiki/桶排序)| `BucketSort` |
|[鸡尾酒排序](https://zh.wikipedia.org/wiki/鸡尾酒排序)| `CocktailShakerSort` |
|[梳排序](https://zh.wikipedia.org/wiki/梳排序)| `CombSort` |
|[计数排序](https://zh.wikipedia.org/wiki/计数排序)| `CountingSort` |
|[侏儒排序](https://zh.wikipedia.org/wiki/侏儒排序)| `GnomeSort` |
|[堆排序](https://zh.wikipedia.org/wiki/堆排序)| `HeapSort` |
|[插入排序](https://zh.wikipedia.org/wiki/插入排序)| `InsertionSort` |
|[归并排序](https://zh.wikipedia.org/wiki/归并排序)| `MergeSort` |
|[鸽巢排序](https://zh.wikipedia.org/wiki/鸽巢排序)| `PigeonholeSort` |
|[快速排序](https://zh.wikipedia.org/wiki/快速排序)| `QuickSort` |
|[基数排序](https://zh.wikipedia.org/wiki/基数排序)| `RadixSort` |
|[选择排序](https://zh.wikipedia.org/wiki/选择排序)| `SelectionSort` |
|[希尔排序](https://zh.wikipedia.org/wiki/希尔排序)| `ShellSort` |
