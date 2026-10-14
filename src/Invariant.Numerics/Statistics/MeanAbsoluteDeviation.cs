namespace Invariant.Numerics.Statistics;

public static class MeanAbsoluteDeviation
{
    public static double Calculate(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        double[] observations = values.ToArray();

        if (observations.Length == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one value.",
                nameof(values));
        }

        double center = Mean.Calculate(observations);

        return Calculate(observations, center);
    }

    public static double Calculate(
        IEnumerable<double> values,
        double center)
    {
        ArgumentNullException.ThrowIfNull(values);

        if (!double.IsFinite(center))
        {
            throw new ArgumentOutOfRangeException(
                nameof(center),
                "The center must be finite.");
        }

        double sum = 0.0;
        long count = 0;

        foreach (double value in values)
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All values must be finite.",
                    nameof(values));
            }

            double deviation = Math.Abs(value - center);

            if (!double.IsFinite(deviation))
            {
                throw new OverflowException(
                    "An absolute deviation exceeds the representable finite double range.");
            }

            count++;

            // Incremental mean avoids overflowing the sum of deviations.
            sum = sum * ((count - 1.0) / count)
                + deviation / count;
        }

        if (count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one value.",
                nameof(values));
        }

        return sum;
    }
}