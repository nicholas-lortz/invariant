namespace Invariant.Numerics.Statistics;

public static class MedianAbsoluteDeviation
{
    public static double Calculate(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        var observations = new List<double>();

        foreach (double value in values)
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All values must be finite.",
                    nameof(values));
            }

            observations.Add(value);
        }

        if (observations.Count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one value.",
                nameof(values));
        }

        double median = Median.Calculate(observations);
        var deviations = new List<double>(observations.Count);

        foreach (double value in observations)
        {
            double deviation = Math.Abs(value - median);

            if (!double.IsFinite(deviation))
            {
                throw new OverflowException(
                    "An absolute deviation exceeds the representable finite double range.");
            }

            deviations.Add(deviation);
        }

        return Median.Calculate(deviations);
    }
}