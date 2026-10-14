namespace Invariant.Numerics.Statistics;

public static class Variance
{
    public static double CalculatePopulation(
        IEnumerable<double> values)
    {
        var (count, mean, sumSquaredDeviations) = CalculateMoments(values);

        if (count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one value.",
                nameof(values));
        }

        return sumSquaredDeviations / count;
    }

    public static double CalculateSample(
        IEnumerable<double> values)
    {
        var (count, mean, sumSquaredDeviations) = CalculateMoments(values);

        if (count < 2)
        {
            throw new ArgumentException(
                "At least two values are required to calculate sample variance.",
                nameof(values));
        }

        return sumSquaredDeviations / (count - 1);
    }

    private static (long Count, double Mean, double M2) CalculateMoments(
        IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        long count = 0;
        double mean = 0.0;
        double m2 = 0.0;

        foreach (double value in values)
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All values must be finite.",
                    nameof(values));
            }

            count++;

            double delta = value - mean;
            mean += delta / count;

            double deltaAfterUpdate = value - mean;
            m2 += delta * deltaAfterUpdate;
        }

        return (count, mean, m2);
    }
}