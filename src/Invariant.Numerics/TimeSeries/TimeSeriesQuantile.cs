namespace Invariant.Numerics.TimeSeries;

public static class TimeSeriesQuantile
{
    public static double Calculate(
        IEnumerable<Observation> observations,
        double probability)
    {
        ArgumentNullException.ThrowIfNull(observations);

        if (!double.IsFinite(probability)
            || probability < 0.0
            || probability > 1.0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(probability),
                "Probability must be finite and between 0 and 1.");
        }

        List<double> values = [];

        foreach (Observation observation in observations)
        {
            if (!double.IsFinite(observation.Value))
            {
                throw new ArgumentException(
                    "All observation values must be finite.",
                    nameof(observations));
            }

            values.Add(observation.Value);
        }

        if (values.Count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one observation.",
                nameof(observations));
        }

        values.Sort();

        if (values.Count == 1)
        {
            return values[0];
        }

        double index = (values.Count - 1) * probability;
        int lowerIndex = (int)index;
        int upperIndex = Math.Min(lowerIndex + 1, values.Count - 1);

        double fraction = index - lowerIndex;
        double lower = values[lowerIndex];
        double upper = values[upperIndex];

        // Interpolate without directly subtracting extreme opposite values.
        return lower * (1.0 - fraction) + upper * fraction;
    }
}