namespace Hua.Sort;

public class PigeonholeSort : ISort<int>
{
    public void Sort(int[] array)
    {
        if (array.Length <= 1)
            return;

        int min = array[0];
        int max = array[0];

        foreach (int value in array)
        {
            if (value < min) min = value;
            if (value > max) max = value;
        }

        int minValue = int.CreateChecked(min);
        int maxValue = int.CreateChecked(max);
        int range = checked(maxValue - minValue + 1);

        var holes = new int[range];

        foreach (int value in array)
        {
            holes[int.CreateChecked(value) - minValue]++;
        }

        int index = 0;

        for (int i = 0; i < holes.Length; i++)
        {
            while (holes[i]-- > 0)
            {
                array[index++] = int.CreateChecked(i + minValue);
            }
        }
    }
}
