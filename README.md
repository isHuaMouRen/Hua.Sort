# Hua. Sort

**Hua. Sort** 是一个使用 C# 实现的排序算法集合，为多种经典排序算法提供统一的封装接口。

## 快速上手

### 添加依赖

在开始之前，你需要在你的项目中引用 `Hua.Sort` 库，你可以直接通过 **Nuget包管理器** 安装:

```bash
Install-Package Hua.Sort
```

或者引用此仓库Release中的 `nupkg` 包或者 `dll` 文件

### 开始编写

引用排序算法所在的命名空间，创建对应排序算法的实例，并调用 `Sort()` 方法即可。

```csharp
using Hua.Sort;
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

若算法支持，可以将 `<int>` 替换为其他支持的类型 *( `double` , `float` , `long` ...)* 来进行排序

## 文档

**Hua. Sort** 提供了一个完整的文档来帮助你使用，请前往 [文档索引](/docs/index.md)
