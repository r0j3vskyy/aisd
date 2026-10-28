using System;

namespace Algorithms;

public static class SelectSort
{
    public static int[] Sort(int[] values)
    {
        ArgumentNullException.ThrowIfNull(values);
        var sorted = (int[])values.Clone();

        for (int i = 0; i < sorted.Length - 1; i++)
        {
            int minimumIndex = i;
            for (int j = i + 1; j < sorted.Length; j++)
            {
                if (sorted[j] < sorted[minimumIndex])
                    minimumIndex = j;
            }

            if (minimumIndex != i)
                (sorted[i], sorted[minimumIndex]) = (sorted[minimumIndex], sorted[i]);
        }

        return sorted;
    }
}
