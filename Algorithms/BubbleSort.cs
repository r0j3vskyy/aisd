using System;

namespace Algorithms;

public static class BubbleSort
{
    public static int[] Sort(int[] values)
    {
        ArgumentNullException.ThrowIfNull(values);
        var sorted = (int[])values.Clone();

        for (int end = sorted.Length - 1; end > 0; end--)
        {
            bool swapped = false;
            for (int i = 0; i < end; i++)
            {
                if (sorted[i] <= sorted[i + 1])
                    continue;

                (sorted[i], sorted[i + 1]) = (sorted[i + 1], sorted[i]);
                swapped = true;
            }

            if (!swapped)
                break;
        }

        return sorted;
    }
}
