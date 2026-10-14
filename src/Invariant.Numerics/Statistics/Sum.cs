namespace Invariant.Numerics.Statistics;

public static class Sum
{
    public static double Calculate(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        double sum = 0.0;

        foreach (double value in values)
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All values must be finite.",
                    nameof(values));
            }

            sum += value;

            if (!double.IsFinite(sum))
            {
                throw new OverflowException(
                    "The sum exceeds the representable finite double range.");
            }
        }

        return sum;
    }
}