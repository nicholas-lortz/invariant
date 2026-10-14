namespace Invariant.Numerics.TimeSeries;

public static class TimeWeightedIntegral
{
    public static double Calculate(IEnumerable<Observation> observations)
    {
        return TimeWeightedSum.Calculate(observations);
    }
}