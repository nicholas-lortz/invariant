namespace Invariant.Numerics.Statistics;

public static class WeightedMean
{
    public static double Calculate(
        IEnumerable<double> values,
        IEnumerable<double> weights)
    {
        ArgumentNullException.ThrowIfNull(values);
        ArgumentNullException.ThrowIfNull(weights);

        using IEnumerator<double> valueEnumerator = values.GetEnumerator();
        using IEnumerator<double> weightEnumerator = weights.GetEnumerator();

        var pairs = new List<(double Value, double Weight)>();
        double maxWeight = 0.0;

        while (true)
        {
            bool hasValue = valueEnumerator.MoveNext();
            bool hasWeight = weightEnumerator.MoveNext();

            if (hasValue != hasWeight)
            {
                throw new ArgumentException(
                    "Values and weights must contain the same number of elements.",
                    nameof(weights));
            }

            if (!hasValue)
            {
                break;
            }

            double value = valueEnumerator.Current;
            double weight = weightEnumerator.Current;

            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All values must be finite.",
                    nameof(values));
            }

            if (!double.IsFinite(weight) || weight < 0.0)
            {
                throw new ArgumentException(
                    "All weights must be finite and nonnegative.",
                    nameof(weights));
            }

            maxWeight = Math.Max(maxWeight, weight);
            pairs.Add((value, weight));
        }

        if (pairs.Count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one value.",
                nameof(values));
        }

        if (maxWeight == 0.0)
        {
            throw new ArgumentException(
                "At least one weight must be positive.",
                nameof(weights));
        }

        double scaledWeightSum = 0.0;
        double scale = 0.0;

        foreach (var pair in pairs)
        {
            scaledWeightSum += pair.Weight / maxWeight;
            scale = Math.Max(scale, Math.Abs(pair.Value));
        }

        if (scale == 0.0)
        {
            return 0.0;
        }

        double weightedSum = 0.0;

        foreach (var pair in pairs)
        {
            double normalizedWeight =
                (pair.Weight / maxWeight) / scaledWeightSum;

            weightedSum += (pair.Value / scale) * normalizedWeight;
        }

        double result = scale * weightedSum;

        if (!double.IsFinite(result))
        {
            throw new OverflowException(
                "The weighted mean exceeds the representable finite double range.");
        }

        return result;
    }
}