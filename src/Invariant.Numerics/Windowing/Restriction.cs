using Invariant.Numerics.TimeSeries;

namespace Invariant.Numerics.Windowing;

/// <summary>
/// Restriction of a sequence of observations to an interval.
/// </summary>
public static class Restriction
{
    /// <summary>
    /// Returns the observations whose timestamps lie in <paramref name="interval"/>,
    /// without copying. The input must be ordered by ascending timestamp.
    /// An observation exactly at <c>interval.Start</c> is included; one exactly at
    /// <c>interval.End</c> is not.
    /// </summary>
    public static ReadOnlyMemory<Observation> Restrict(
        this ReadOnlyMemory<Observation> observations,
        Interval interval)
    {
        ReadOnlySpan<Observation> span = observations.Span;

        int first = LowerBound(span, interval.Start);
        int end = LowerBound(span, interval.End);

        return observations.Slice(first, end - first);
    }

    // Index of the first observation with Timestamp >= timestamp (span.Length if none).
    private static int LowerBound(ReadOnlySpan<Observation> span, DateTimeOffset timestamp)
    {
        int low = 0;
        int high = span.Length;

        while (low < high)
        {
            int middle = low + ((high - low) >> 1);

            if (span[middle].Timestamp < timestamp)
            {
                low = middle + 1;
            }
            else
            {
                high = middle;
            }
        }

        return low;
    }
}