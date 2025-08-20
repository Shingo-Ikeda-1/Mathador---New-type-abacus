using System;
using System.Collections.Generic;

public static class DigitExtractor
{
    // Sends out digits of a digitNumber one by one from the last digits. Use {this Method}().Reverse() to get them from the first digit.
    public static IEnumerable<int> GetDigitsFromLast(int source)
    {
        while (source > 0)
        {
            var digit = Math.Abs(source) % 10;
            source /= 10;
            yield return digit;
        }
    }

    public static int GetDigit(int source, int digitPosition)
    {
        source %= (int)Math.Pow(10, digitPosition + 1);
        source /= (int)Math.Pow(10, digitPosition);
        return source;
    }

    /// <summary>
    /// Swaps one digitPosition in source with digit
    /// </summary>
    /// <param name="source"></param>
    /// <param name="digitPosition"></param>
    /// <param name="digit"></param>
    /// <returns></returns>
    public static int SwapOneDigit(int source, int digitPosition, int digit)
    {
        var enumerator = GetDigitsFromLast(source).GetEnumerator();
        int total = 0;
        int i = 0;
        do
        {
            if (i == digitPosition)
            {
                total += (int)Math.Pow(10, i) * digit;
                enumerator.MoveNext();
            }
            else if (enumerator.MoveNext())
            {
                total += (int)Math.Pow(10, i) * enumerator.Current;
            }
            i++;
        } while (i < digitPosition + 1);

        while (enumerator.MoveNext())
        {
            total += (int)Math.Pow(10, i) * enumerator.Current;
            i++;
        }

        return total;
    }

    public static int ConvertListIntoInt(List<int> source)
    {
        int total = 0;
        foreach (int entry in source)
        {
            total = 10 * total + entry;
        }
        return total;
    }
}