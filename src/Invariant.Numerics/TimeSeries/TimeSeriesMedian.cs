namespace Invariant.Numerics.TimeSeries;

public static class TimeSeriesMedian
{
    public static double Calculate(IEnumerable<Observation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

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

        int middle = values.Count / 2;

        if (values.Count % 2 != 0)
        {
            return values[middle];
        }

        double lower = values[middle - 1];
        double upper = values[middle];

        // Divide before adding to avoid overflow for extreme values.
        return lower / 2.0 + upper / 2.0;
    }
}