using System;

namespace Algorithms;

public static class MergeSort
{
    public static int[] Sort(int[] values)
    {
        ArgumentNullException.ThrowIfNull(values);
        var sorted = (int[])values.Clone();
        MergeSortRange(sorted, 0, sorted.Length - 1);
        return sorted;
    }

    private static void MergeSortRange(int[] values, int left, int right)
    {
        if (left >= right)
            return;

        int middle = left + (right - left) / 2;
        MergeSortRange(values, left, middle);
        MergeSortRange(values, middle + 1, right);
        Merge(values, left, middle, right);
    }

    private static void Merge(int[] values, int left, int middle, int right)
    {
        var merged = new int[right - left + 1];
        int leftIndex = left;
        int rightIndex = middle + 1;
        int mergedIndex = 0;

        while (leftIndex <= middle && rightIndex <= right)
        {
            merged[mergedIndex++] =
                values[leftIndex] <= values[rightIndex]
                    ? values[leftIndex++]
                    : values[rightIndex++];
        }

        while (leftIndex <= middle)
            merged[mergedIndex++] = values[leftIndex++];
        while (rightIndex <= right)
            merged[mergedIndex++] = values[rightIndex++];

        Array.Copy(merged, 0, values, left, merged.Length);
    }
}
