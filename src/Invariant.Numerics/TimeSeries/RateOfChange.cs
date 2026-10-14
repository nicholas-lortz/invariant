namespace Invariant.Numerics.TimeSeries;

public static class RateOfChange
{
    /// <summary>
    /// Calculates the rate of change between two observations,
    /// expressed in value units per second.
    /// </summary>
    public static double Calculate(
        Observation start,
        Observation end)
    {
        if (!double.IsFinite(start.Value))
        {
            throw new ArgumentException(
                "Observation values must be finite.",
                nameof(start));
        }

        if (!double.IsFinite(end.Value))
        {
            throw new ArgumentException(
                "Observation values must be finite.",
                nameof(end));
        }

        long elapsedTicks =
            end.Timestamp.Ticks - start.Timestamp.Ticks;

        if (elapsedTicks <= 0)
        {
            throw new ArgumentException(
                "The end timestamp must be later than the start timestamp.",
                nameof(end));
        }

        double elapsedSeconds =
            (double)elapsedTicks / TimeSpan.TicksPerSecond;

        double rate =
            (end.Value / elapsedSeconds) -
            (start.Value / elapsedSeconds);

        if (!double.IsFinite(rate))
        {
            throw new OverflowException(
                "The rate of change exceeds the representable finite double range.");
        }

        return rate;
    }
}