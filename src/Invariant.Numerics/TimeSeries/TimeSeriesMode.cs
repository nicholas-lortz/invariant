namespace Invariant.Numerics.TimeSeries;

public static class TimeSeriesMode
{
    public static IReadOnlyList<double> Calculate(
        IEnumerable<Observation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

        var frequencies = new Dictionary<double, int>();

        foreach (Observation observation in observations)
        {
            double value = observation.Value;

            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All observation values must be finite.",
                    nameof(observations));
            }

            frequencies.TryGetValue(value, out int frequency);
            frequencies[value] = frequency + 1;
        }

        if (frequencies.Count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one observation.",
                nameof(observations));
        }

        int maximumFrequency = frequencies.Values.Max();

        return frequencies
            .Where(pair => pair.Value == maximumFrequency)
            .Select(pair => pair.Key)
            .OrderBy(value => value)
            .ToArray();
    }
}