namespace Invariant.Numerics.Statistics;

public static class RootMeanSquare
{
    public static double Calculate(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        double scale = 0.0;
        long count = 0;

        // Find the scale while validating the input.
        foreach (double value in values)
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All values must be finite.",
                    nameof(values));
            }

            scale = Math.Max(scale, Math.Abs(value));
            count = checked(count + 1);
        }

        if (count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one value.",
                nameof(values));
        }

        if (scale == 0.0)
        {
            return 0.0;
        }

        // Normalize before squaring to prevent intermediate overflow.
        double sumSquared = 0.0;

        foreach (double value in values)
        {
            double normalized = value / scale;
            sumSquared += normalized * normalized;
        }

        double meanSquared = sumSquared / count;

        return scale * Math.Sqrt(meanSquared);
    }
}