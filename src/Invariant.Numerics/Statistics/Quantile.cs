namespace Invariant.Numerics.Statistics;

public static class Quantile
{
    /// <summary>
    /// Calculates a quantile using linear interpolation with
    /// position h = (n - 1) * probability.
    /// </summary>
    /// <param name="values">The input values.</param>
    /// <param name="probability">A probability between 0 and 1, inclusive.</param>
    public static double Calculate(
        IEnumerable<double> values,
        double probability)
    {
        ArgumentNullException.ThrowIfNull(values);

        if (!double.IsFinite(probability) ||
            probability < 0.0 ||
            probability > 1.0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(probability),
                "Probability must be finite and between 0 and 1.");
        }

        var sorted = new List<double>();

        foreach (double value in values)
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All values must be finite.",
                    nameof(values));
            }

            sorted.Add(value);
        }

        if (sorted.Count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one value.",
                nameof(values));
        }

        sorted.Sort();

        if (sorted.Count == 1 || probability == 0.0)
        {
            return sorted[0];
        }

        if (probability == 1.0)
        {
            return sorted[^1];
        }

        double position = (sorted.Count - 1) * probability;
        int lowerIndex = (int)position;
        int upperIndex = lowerIndex + 1;
        double fraction = position - lowerIndex;

        double lower = sorted[lowerIndex];
        double upper = sorted[upperIndex];

        // Avoid the potentially overflowing expression
        // lower + fraction * (upper - lower).
        return (1.0 - fraction) * lower + fraction * upper;
    }
}