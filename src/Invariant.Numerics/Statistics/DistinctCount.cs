namespace Invariant.Numerics.Statistics;

public static class DistinctCount
{
    public static long Calculate(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        var distinct = new HashSet<double>();

        foreach (double value in values)
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All values must be finite.",
                    nameof(values));
            }

            distinct.Add(value);
        }

        return distinct.Count;
    }
}