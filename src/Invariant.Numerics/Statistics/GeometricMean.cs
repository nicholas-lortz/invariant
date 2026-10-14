namespace Invariant.Numerics.Statistics;

public static class GeometricMean
{
    public static double Calculate(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        long count = 0;
        double meanLog = 0.0;
        double firstValue = 0.0;
        bool allEqual = true;

        foreach (double value in values)
        {
            if (!double.IsFinite(value) || value <= 0.0)
            {
                throw new ArgumentException(
                    "All values must be finite and greater than zero.",
                    nameof(values));
            }

            if (count == 0)
            {
                firstValue = value;
            }
            else if (value != firstValue)
            {
                allEqual = false;
            }

            count++;

            meanLog += (Math.Log(value) - meanLog) / count;
        }

        if (count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one value.",
                nameof(values));
        }

        if (allEqual)
        {
            return firstValue;
        }

        return Math.Exp(meanLog);
    }
}