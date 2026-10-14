namespace Invariant.Numerics.TimeSeries;

public static class LogReturn
{
    public static IReadOnlyList<Observation> Calculate(
        IEnumerable<Observation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

        var result = new List<Observation>();

        bool hasPrevious = false;
        Observation previous = default;

        foreach (Observation current in observations)
        {
            if (hasPrevious)
            {
                if (current.Timestamp < previous.Timestamp)
                {
                    throw new ArgumentException(
                        "Observations must be in chronological order.",
                        nameof(observations));
                }

                if (previous.Value <= 0.0 || current.Value <= 0.0)
                {
                    throw new ArgumentException(
                        "Values must be greater than zero to calculate log returns.",
                        nameof(observations));
                }

                double logReturn =
                    Math.Log(current.Value) - Math.Log(previous.Value);

                if (!double.IsFinite(logReturn))
                {
                    throw new OverflowException(
                        "The log return is not representable as a finite double.");
                }

                result.Add(new Observation(
                    current.Timestamp,
                    logReturn));
            }

            previous = current;
            hasPrevious = true;
        }

        return result.AsReadOnly();
    }
}