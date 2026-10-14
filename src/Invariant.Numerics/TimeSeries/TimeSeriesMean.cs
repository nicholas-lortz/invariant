namespace Invariant.Numerics.TimeSeries;

public static class TimeSeriesMean
{
    public static double Calculate(IEnumerable<Observation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

        long count = 0;
        double mean = 0.0;
        double compensation = 0.0;

        foreach (Observation observation in observations)
        {
            double value = observation.Value;

            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All observation values must be finite.",
                    nameof(observations));
            }

            count++;

            // Scale both operands before combining them to avoid
            // overflowing when values approach double.MaxValue.
            double term = value / count - mean / count;
            double adjusted = term - compensation;
            double nextMean = mean + adjusted;

            compensation = (nextMean - mean) - adjusted;
            mean = nextMean;
        }

        if (count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one observation.",
                nameof(observations));
        }

        return mean;
    }
}