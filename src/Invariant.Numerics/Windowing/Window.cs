namespace Invariant.Numerics.Windowing;

/// <summary>
/// Constructs sequences of intervals (windows) over a domain.
/// </summary>
public static class Window
{
    /// <summary>
    /// Divides <paramref name="domain"/> into contiguous, disjoint windows of equal
    /// <paramref name="duration"/>, ordered by start. Together the windows cover the
    /// domain exactly.
    /// </summary>
    /// <param name="domain">The interval to divide.</param>
    /// <param name="duration">The duration of each window. Must be positive and divide
    /// the duration of <paramref name="domain"/> exactly.</param>
    public static IReadOnlyList<Interval> Partition(Interval domain, TimeSpan duration)
    {
        if (duration <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(
                nameof(duration), duration, "The window duration must be positive.");
        }

        if (domain.Duration.Ticks % duration.Ticks != 0)
        {
            throw new ArgumentException(
                "The domain duration must be an exact multiple of the window duration.",
                nameof(duration));
        }

        int count = checked((int)(domain.Duration.Ticks / duration.Ticks));
        var windows = new Interval[count];

        for (int index = 0; index < count; index++)
        {
            DateTimeOffset start = domain.Start.AddTicks(duration.Ticks * index);
            windows[index] = new Interval(start, start.AddTicks(duration.Ticks));
        }

        return windows;
    }
}