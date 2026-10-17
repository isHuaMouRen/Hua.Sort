# Hua.Sort

[简体中文](/README.md) | **English**

---

**Hua.Sort** is a collection of sorting algorithms implemented in C#, providing a unified interface for various classic sorting algorithms.

## Usage

### Write Your Own Code

Import the `Hua.Sort` namespace, instantiate the desired sorting algorithm, and call its `Sort()` method.

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

Output:

```text
0, 1, 2, 3, 4, 5, 6, 7, 8, 9
```

This example uses `BubbleSort<int>` to perform bubble sort. You can replace `BubbleSort` with the class name of another sorting algorithm to use a different sorting method.

Where supported by the algorithm, you can replace `int` with other supported numeric types, such as `double` , `float` , or `long` .

### Explore the Demo

Clone this repository and open the solution in **Visual Studio**. Then, navigate to the `Hua.Sort.Demo` project to explore the example code, build the project, and run the tests.

## Available Algorithms

*(The class name is the algorithm name without spaces.)*

| Algorithm | 
|-|
| `Bubble Sort` |
| `Bucket Sort` |
| `CocktailShaker Sort` |
| `Comb Sort` |
| `Counting Sort` |
| `Gnome Sort` |
| `Heap Sort` |
| `Insertion Sort` |
| `Merge Sort` |
| `Pigeonhole Sort` |
| `Quick Sort` |
| `Radix Sort` |
| `Selection Sort` |
| `Shell Sort` |
