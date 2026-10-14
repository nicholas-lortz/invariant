namespace Invariant.Numerics.Statistics;

public static class HarmonicMean
{
    public static double Calculate(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        var observations = new List<double>();
        double minimum = double.PositiveInfinity;

        foreach (double value in values)
        {
            if (!double.IsFinite(value) || value <= 0.0)
            {
                throw new ArgumentException(
                    "All values must be finite and greater than zero.",
                    nameof(values));
            }

            observations.Add(value);
            minimum = Math.Min(minimum, value);
        }

        if (observations.Count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one value.",
                nameof(values));
        }

        double meanRatio = 0.0;
        long count = 0;

        foreach (double value in observations)
        {
            count++;
            double ratio = minimum / value;
            meanRatio += (ratio - meanRatio) / count;
        }

        return minimum / meanRatio;
    }
}