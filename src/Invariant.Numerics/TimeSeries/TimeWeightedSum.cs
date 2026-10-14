namespace Invariant.Numerics.TimeSeries;

public static class TimeWeightedSum
{
    public static double Calculate(IEnumerable<Observation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

        var items = new List<Observation>();

        foreach (Observation observation in observations)
        {
            if (!double.IsFinite(observation.Value))
            {
                throw new ArgumentException(
                    "All observation values must be finite.",
                    nameof(observations));
            }

            items.Add(observation);
        }

        if (items.Count < 2)
        {
            throw new ArgumentException(
                "At least two observations are required.",
                nameof(observations));
        }

        double sum = 0.0;

        for (int i = 0; i < items.Count - 1; i++)
        {
            Observation current = items[i];
            Observation next = items[i + 1];

            TimeSpan interval = next.Timestamp - current.Timestamp;

            if (interval <= TimeSpan.Zero)
            {
                throw new ArgumentException(
                    "Observation timestamps must be strictly increasing.",
                    nameof(observations));
            }

            double durationHours = interval.TotalHours;
            double contribution = current.Value * durationHours;

            if (!double.IsFinite(contribution))
            {
                throw new OverflowException(
                    "A time-weighted contribution exceeds the representable finite double range.");
            }

            sum += contribution;

            if (!double.IsFinite(sum))
            {
                throw new OverflowException(
                    "The time-weighted sum exceeds the representable finite double range.");
            }
        }

        return sum;
    }
}