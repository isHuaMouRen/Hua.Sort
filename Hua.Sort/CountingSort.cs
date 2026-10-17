namespace Hua.Sort;

public class CountingSort : ISort<int>
{
    public void Sort(int[] array)
    {
        if (array.Length <= 1)
            return;

        int min = int.CreateChecked(array[0]);
        int max = min;

        foreach (int value in array)
        {
            int number = int.CreateChecked(value);

            if (number < min) min = number;
            if (number > max) max = number;
        }

        int range = checked(max - min + 1);
        var counts = new int[range];

        foreach (int value in array)
        {
            counts[int.CreateChecked(value) - min]++;
        }

        int index = 0;

        for (int i = 0; i < counts.Length; i++)
        {
            while (counts[i]-- > 0)
            {
                array[index++] = int.CreateChecked(i + min);
            }
        }
    }
}
