namespace Invariant.Numerics.TimeSeries;

public static class TimeSeriesSum
{
    public static double Calculate(IEnumerable<Observation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

        double sum = 0.0;

        foreach (Observation observation in observations)
        {
            double value = observation.Value;

            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All observation values must be finite.",
                    nameof(observations));
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