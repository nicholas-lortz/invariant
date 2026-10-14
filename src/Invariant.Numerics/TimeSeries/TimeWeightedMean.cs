namespace Invariant.Numerics.TimeSeries;

public static class TimeWeightedMean
{
    public static double Calculate(
        IEnumerable<Observation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

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
        double weightedMean = 0.0;

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

            double intervalWeight =
                (double)intervalTicks / totalTicks;

            weightedMean =
                weightedMean * (1.0 - intervalWeight)
                + previous.Value * intervalWeight;

            previous = current;
        }

        if (totalTicks == 0)
        {
            throw new ArgumentException(
                "At least two observations are required.",
                nameof(observations));
        }

        return weightedMean;
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