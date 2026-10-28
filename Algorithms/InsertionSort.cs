using System;

namespace Algorithms;

public static class InsertionSort
{
    public static int[] Sort(int[] values)
    {
        ArgumentNullException.ThrowIfNull(values);
        var sorted = (int[])values.Clone();

        for (int i = 1; i < sorted.Length; i++)
        {
            int current = sorted[i];
            int j = i - 1;

            while (j >= 0 && sorted[j] > current)
            {
                sorted[j + 1] = sorted[j];
                j--;
            }

            sorted[j + 1] = current;
        }

        return sorted;
    }
}
