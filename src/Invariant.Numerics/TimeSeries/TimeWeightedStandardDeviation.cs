namespace Invariant.Numerics.TimeSeries;

public static class TimeWeightedStandardDeviation
{
    public static double Calculate(
        IEnumerable<Observation> observations)
    {
        double variance = TimeWeightedVariance.Calculate(observations);

        return Math.Sqrt(variance);
    }
}