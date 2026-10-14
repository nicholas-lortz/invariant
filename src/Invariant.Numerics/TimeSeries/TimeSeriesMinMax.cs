namespace Invariant.Numerics.TimeSeries;

public sealed record TimeSeriesMinMaxResult(
    Observation Minimum,
    Observation Maximum);

public static class TimeSeriesMinMax
{
    public static TimeSeriesMinMaxResult Calculate(
        IEnumerable<Observation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

        using IEnumerator<Observation> enumerator =
            observations.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            throw new ArgumentException(
                "The collection must contain at least one observation.",
                nameof(observations));
        }

        Observation first = enumerator.Current;

        if (!double.IsFinite(first.Value))
        {
            throw new ArgumentException(
                "All observation values must be finite.",
                nameof(observations));
        }

        Observation minimum = first;
        Observation maximum = first;

        while (enumerator.MoveNext())
        {
            Observation current = enumerator.Current;

            if (!double.IsFinite(current.Value))
            {
                throw new ArgumentException(
                    "All observation values must be finite.",
                    nameof(observations));
            }

            if (current.Value < minimum.Value)
            {
                minimum = current;
            }

            if (current.Value > maximum.Value)
            {
                maximum = current;
            }
        }

        return new TimeSeriesMinMaxResult(minimum, maximum);
    }
}