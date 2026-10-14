namespace Invariant.Numerics.TimeSeries;

public readonly record struct Observation
{
    public DateTimeOffset Timestamp { get; }

    public double Value { get; }

    public Observation(DateTimeOffset timestamp, double value)
    {
        Timestamp = timestamp;
        Value = value;
    }
}