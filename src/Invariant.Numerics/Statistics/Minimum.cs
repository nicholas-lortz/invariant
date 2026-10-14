namespace Invariant.Numerics.Statistics;

public static class Minimum
{
    public static double Calculate(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        bool hasValue = false;
        double minimum = double.PositiveInfinity;

        foreach (double value in values)
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All values must be finite.",
                    nameof(values));
            }

            if (!hasValue || value < minimum)
            {
                minimum = value;
                hasValue = true;
            }
        }

        if (!hasValue)
        {
            throw new ArgumentException(
                "The collection must contain at least one value.",
                nameof(values));
        }

        return minimum;
    }
}