using System;

namespace Algorithms;

public static class CountingSort
{
    public static int[] Sort(int[] values)
    {
        ArgumentNullException.ThrowIfNull(values);
        if (values.Length == 0)
            return Array.Empty<int>();

        int minimum = values[0];
        int maximum = values[0];
        foreach (int value in values)
        {
            minimum = Math.Min(minimum, value);
            maximum = Math.Max(maximum, value);
        }

        long range = (long)maximum - minimum + 1;
        if (range > int.MaxValue)
        {
            throw new ArgumentException(
                "The value range is too large for counting sort.",
                nameof(values));
        }

        var counts = new int[(int)range];
        foreach (int value in values)
            counts[value - minimum]++;

        var sorted = new int[values.Length];
        int index = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            while (counts[i]-- > 0)
                sorted[index++] = i + minimum;
        }

        return sorted;
    }
}
