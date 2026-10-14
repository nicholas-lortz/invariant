namespace Invariant.Numerics.Statistics;

public static class Mean
{
    public static double Calculate(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        double mean = 0.0;
        long count = 0;

        foreach (double value in values)
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All values must be finite.",
                    nameof(values));
            }

            count++;

            if (count == 1)
            {
                mean = value;
            }
            else
            {
                double n = count;
                mean = mean * ((n - 1.0) / n) + value / n;
            }
        }

        if (count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one value.",
                nameof(values));
        }

        return mean;
    }
}