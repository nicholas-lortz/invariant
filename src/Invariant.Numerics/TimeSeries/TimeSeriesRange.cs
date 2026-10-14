namespace Invariant.Numerics.TimeSeries;

public static class TimeSeriesRange
{
    public static double Calculate(IEnumerable<Observation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

        bool hasObservation = false;
        double minimum = double.PositiveInfinity;
        double maximum = double.NegativeInfinity;

        foreach (Observation observation in observations)
        {
            double value = observation.Value;

            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All observation values must be finite.",
                    nameof(observations));
            }

            hasObservation = true;
            minimum = Math.Min(minimum, value);
            maximum = Math.Max(maximum, value);
        }

        if (!hasObservation)
        {
            throw new ArgumentException(
                "The collection must contain at least one observation.",
                nameof(observations));
        }

        double range = maximum - minimum;

        if (!double.IsFinite(range))
        {
            throw new OverflowException(
                "The range exceeds the representable finite double range.");
        }

        return range;
    }
}