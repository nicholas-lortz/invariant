namespace Invariant.Numerics.Statistics;

public static class Count
{
    public static long Calculate(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        long count = 0;

        foreach (double value in values)
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All values must be finite.",
                    nameof(values));
            }

            count = checked(count + 1);
        }

        return count;
    }
}