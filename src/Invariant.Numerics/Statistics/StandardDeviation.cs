namespace Invariant.Numerics.Statistics;

public static class StandardDeviation
{
    public static double CalculatePopulation(
        IEnumerable<double> values)
    {
        return Math.Sqrt(Variance.CalculatePopulation(values));
    }

    public static double CalculateSample(
        IEnumerable<double> values)
    {
        return Math.Sqrt(Variance.CalculateSample(values));
    }
}