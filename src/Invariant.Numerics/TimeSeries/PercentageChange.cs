namespace Invariant.Numerics.TimeSeries;

public static class PercentageChange
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

                if (previous.Value == 0.0)
                {
                    throw new ArgumentException(
                        "Previous values must not be zero.",
                        nameof(observations));
                }

                double change =
                    (current.Value - previous.Value) / previous.Value;

                if (!double.IsFinite(change))
                {
                    throw new OverflowException(
                        "The percentage change exceeds the representable finite double range.");
                }

                result.Add(new Observation(
                    current.Timestamp,
                    change));
            }

            previous = current;
            hasPrevious = true;
        }

        return result.AsReadOnly();
    }
}