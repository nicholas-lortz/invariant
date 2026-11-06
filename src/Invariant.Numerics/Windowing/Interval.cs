namespace Invariant.Numerics.Windowing;

/// <summary>
/// A half-open interval of time, [<see cref="Start"/>, <see cref="End"/>).
/// An instant equal to <see cref="Start"/> belongs to the interval;
/// an instant equal to <see cref="End"/> does not.
/// Adjacent intervals therefore neither overlap nor leave gaps.
/// </summary>
public readonly record struct Interval
{
    public Interval(DateTimeOffset start, DateTimeOffset end)
    {
        if (end <= start)
        {
            throw new ArgumentOutOfRangeException(
                nameof(end), end, "The end of an interval must be after its start.");
        }

        Start = start;
        End = end;
    }

    public DateTimeOffset Start { get; }

    public DateTimeOffset End { get; }

    public TimeSpan Duration => End - Start;

    public bool Contains(DateTimeOffset timestamp) => timestamp >= Start && timestamp < End;
}