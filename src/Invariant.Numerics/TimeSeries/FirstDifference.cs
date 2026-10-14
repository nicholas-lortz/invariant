namespace Invariant.Numerics.TimeSeries;

public static class FirstDifference
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

                double difference = current.Value - previous.Value;

                if (!double.IsFinite(difference))
                {
                    throw new OverflowException(
                        "The difference exceeds the representable finite double range.");
                }

                result.Add(new Observation(
                    current.Timestamp,
                    difference));
            }

            previous = current;
            hasPrevious = true;
        }

        return result.AsReadOnly();
    }
}