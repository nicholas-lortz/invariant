namespace Invariant.Numerics.Statistics;

public static class Range
{
    public static double Calculate(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        bool hasValue = false;
        double minimum = 0.0;
        double maximum = 0.0;

        foreach (double value in values)
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All values must be finite.",
                    nameof(values));
            }

            if (!hasValue)
            {
                minimum = value;
                maximum = value;
                hasValue = true;
                continue;
            }

            if (value < minimum)
            {
                minimum = value;
            }

            if (value > maximum)
            {
                maximum = value;
            }
        }

        if (!hasValue)
        {
            throw new ArgumentException(
                "The collection must contain at least one value.",
                nameof(values));
        }

        double result = maximum - minimum;

        if (!double.IsFinite(result))
        {
            throw new OverflowException(
                "The range exceeds the representable finite double range.");
        }

        return result;
    }
}