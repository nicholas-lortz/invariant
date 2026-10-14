namespace Invariant.Numerics.TimeSeries;

public static class TimeWeightedQuantile
{
    public static double Calculate(
        IEnumerable<Observation> observations,
        DateTimeOffset endTimestamp,
        double probability)
    {
        ArgumentNullException.ThrowIfNull(observations);

        if (!double.IsFinite(probability)
            || probability < 0.0
            || probability > 1.0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(probability),
                "Probability must be finite and between 0 and 1.");
        }

        var durations = new List<(double Value, double Duration)>();
        DateTimeOffset? previousTimestamp = null;

        foreach (Observation observation in observations)
        {
            if (!double.IsFinite(observation.Value))
            {
                throw new ArgumentException(
                    "All observation values must be finite.",
                    nameof(observations));
            }

            if (previousTimestamp.HasValue &&
                observation.Timestamp <= previousTimestamp.Value)
            {
                throw new ArgumentException(
                    "Observation timestamps must be strictly increasing.",
                    nameof(observations));
            }

            if (previousTimestamp.HasValue)
            {
                Observation previous = durations.Count > 0
                    ? new Observation(
                        previousTimestamp.Value,
                        durations[^1].Value)
                    : default;

                double duration =
                    (observation.Timestamp - previousTimestamp.Value).Ticks;

                durations.Add((previous.Value, duration));
            }

            // Store the current value temporarily with zero duration.
            durations.Add((observation.Value, 0.0));
            previousTimestamp = observation.Timestamp;
        }

        if (durations.Count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one observation.",
                nameof(observations));
        }

        if (endTimestamp <= previousTimestamp!.Value)
        {
            throw new ArgumentException(
                "The end timestamp must be later than the final observation.",
                nameof(endTimestamp));
        }

        // Reconstruct the value intervals from the original observations.
        // The final observation extends to endTimestamp.
        var intervals = new List<(double Value, double Duration)>();
        DateTimeOffset? intervalStart = null;
        double intervalValue = 0.0;

        foreach (var entry in durations)
        {
            if (entry.Duration == 0.0)
            {
                if (intervalStart.HasValue)
                {
                    intervals.Add((
                        intervalValue,
                        (previousTimestamp.Value - intervalStart.Value).Ticks));
                }

                intervalStart = intervalStart.HasValue
                    ? intervalStart.Value
                    : previousTimestamp.Value;

                intervalValue = entry.Value;
            }
        }

        // Build the weighted distribution directly.
        intervals.Clear();
        var observationsList = new List<Observation>(observations);

        for (int i = 0; i < observationsList.Count; i++)
        {
            DateTimeOffset nextTimestamp =
                i + 1 < observationsList.Count
                    ? observationsList[i + 1].Timestamp
                    : endTimestamp;

            intervals.Add((
                observationsList[i].Value,
                (nextTimestamp - observationsList[i].Timestamp).Ticks));
        }

        intervals.Sort((a, b) => a.Value.CompareTo(b.Value));

        double totalDuration = 0.0;

        foreach (var interval in intervals)
        {
            totalDuration += interval.Duration;
        }

        double targetDuration = probability * totalDuration;
        double cumulativeDuration = 0.0;

        foreach (var interval in intervals)
        {
            cumulativeDuration += interval.Duration;

            if (cumulativeDuration >= targetDuration)
            {
                return interval.Value;
            }
        }

        return intervals[^1].Value;
    }
}