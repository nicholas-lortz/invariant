namespace Invariant.Numerics.TimeSeries;

public static class TimeWeightedMinimum
{
    public static double Calculate(IEnumerable<Observation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

        using IEnumerator<Observation> enumerator = observations.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            throw new ArgumentException(
                "At least two observations are required.",
                nameof(observations));
        }

        Observation previous = enumerator.Current;
        ValidateValue(previous.Value);

        double minimum = previous.Value;
        bool hasInterval = false;

        while (enumerator.MoveNext())
        {
            Observation current = enumerator.Current;
            ValidateValue(current.Value);

            if (current.Timestamp <= previous.Timestamp)
            {
                throw new ArgumentException(
                    "Observation timestamps must be strictly increasing.",
                    nameof(observations));
            }

            minimum = Math.Min(minimum, previous.Value);
            hasInterval = true;
            previous = current;
        }

        if (!hasInterval)
        {
            throw new ArgumentException(
                "At least two observations are required.",
                nameof(observations));
        }

        return minimum;
    }

    private static void ValidateValue(double value)
    {
        if (!double.IsFinite(value))
        {
            throw new ArgumentException(
                "All observation values must be finite.");
        }
    }
}