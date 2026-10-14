namespace Invariant.Numerics.TimeSeries;

public static class TimeWeightedVariance
{
    public static double Calculate(
        IEnumerable<Observation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

        var intervals = new List<(double Value, long Ticks)>();

        using IEnumerator<Observation> enumerator =
            observations.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            throw new ArgumentException(
                "The collection must contain at least two observations.",
                nameof(observations));
        }

        Observation previous = enumerator.Current;
        ValidateObservation(previous, nameof(observations));

        long totalTicks = 0;

        while (enumerator.MoveNext())
        {
            Observation current = enumerator.Current;
            ValidateObservation(current, nameof(observations));

            long intervalTicks =
                current.Timestamp.Ticks - previous.Timestamp.Ticks;

            if (intervalTicks <= 0)
            {
                throw new ArgumentException(
                    "Observation timestamps must be strictly increasing.",
                    nameof(observations));
            }

            totalTicks = checked(totalTicks + intervalTicks);

            intervals.Add((previous.Value, intervalTicks));
            previous = current;
        }

        if (intervals.Count == 0 || totalTicks <= 0)
        {
            throw new ArgumentException(
                "The collection must contain at least two observations.",
                nameof(observations));
        }

        // Calculate the time-weighted mean.
        double mean = 0.0;

        foreach (var interval in intervals)
        {
            double weight = (double)interval.Ticks / totalTicks;
            mean += interval.Value * weight;
        }

        // Calculate the time-weighted population variance.
        double variance = 0.0;

        foreach (var interval in intervals)
        {
            double weight = (double)interval.Ticks / totalTicks;
            double difference = interval.Value - mean;

            variance += weight * difference * difference;
        }

        if (!double.IsFinite(variance))
        {
            throw new OverflowException(
                "The time-weighted variance exceeds the representable finite double range.");
        }

        return variance;
    }

    private static void ValidateObservation(
        Observation observation,
        string parameterName)
    {
        if (!double.IsFinite(observation.Value))
        {
            throw new ArgumentException(
                "Observation values must be finite.",
                parameterName);
        }
    }
}