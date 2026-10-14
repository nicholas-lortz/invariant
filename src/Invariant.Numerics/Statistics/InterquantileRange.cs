namespace Invariant.Numerics.Statistics;

public static class InterquartileRange
{
    public static double Calculate(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        // Materialize once so both quantiles use the same observations.
        double[] observations = values is double[] array
            ? (double[])array.Clone()
            : new List<double>(values).ToArray();

        double firstQuartile = Quantile.Calculate(observations, 0.25);
        double thirdQuartile = Quantile.Calculate(observations, 0.75);

        double result = thirdQuartile - firstQuartile;

        if (!double.IsFinite(result))
        {
            throw new OverflowException(
                "The interquartile range exceeds the representable finite double range.");
        }

        return result;
    }
}