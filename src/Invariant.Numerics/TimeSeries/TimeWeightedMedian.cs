namespace Invariant.Numerics.TimeSeries;

public static class TimeWeightedMedian
{
    public static double Calculate(
        IEnumerable<Observation> observations,
        DateTimeOffset endTimestamp)
    {
        ArgumentNullException.ThrowIfNull(observations);

        List<Observation> items = [];

        foreach (Observation observation in observations)
        {
            if (!double.IsFinite(observation.Value))
            {
                throw new ArgumentException(
                    "All observation values must be finite.",
                    nameof(observations));
            }

            if (items.Count > 0 &&
                observation.Timestamp <= items[^1].Timestamp)
            {
                throw new ArgumentException(
                    "Observation timestamps must be strictly increasing.",
                    nameof(observations));
            }

            items.Add(observation);
        }

        if (items.Count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one observation.",
                nameof(observations));
        }

        if (endTimestamp <= items[^1].Timestamp)
        {
            throw new ArgumentException(
                "The end timestamp must be later than the final observation.",
                nameof(endTimestamp));
        }

        var durations = new List<(double Value, double Duration)>();
        double totalDuration = 0.0;

        for (int i = 0; i < items.Count; i++)
        {
            DateTimeOffset nextTimestamp =
                i + 1 < items.Count
                    ? items[i + 1].Timestamp
                    : endTimestamp;

            double duration =
                (nextTimestamp - items[i].Timestamp).Ticks;

            durations.Add((items[i].Value, duration));
            totalDuration += duration;
        }

        durations.Sort((a, b) => a.Value.CompareTo(b.Value));

        double cumulativeDuration = 0.0;
        double halfway = totalDuration / 2.0;

        foreach (var item in durations)
        {
            cumulativeDuration += item.Duration;

            if (cumulativeDuration >= halfway)
            {
                return item.Value;
            }
        }

        throw new InvalidOperationException(
            "Unable to calculate the time-weighted median.");
    }
}