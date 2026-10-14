namespace Invariant.Numerics.Statistics;

public static class Mode
{
    public static IReadOnlyList<double> Calculate(
        IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        var frequencies = new Dictionary<double, int>();

        foreach (double value in values)
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All values must be finite.",
                    nameof(values));
            }

            frequencies.TryGetValue(value, out int count);
            frequencies[value] = count + 1;
        }

        if (frequencies.Count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one value.",
                nameof(values));
        }

        int maximumFrequency = frequencies.Values.Max();

        return frequencies
            .Where(pair => pair.Value == maximumFrequency)
            .Select(pair => pair.Key)
            .OrderBy(value => value)
            .ToArray();
    }
}