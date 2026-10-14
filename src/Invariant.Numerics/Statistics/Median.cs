namespace Invariant.Numerics.Statistics;

public static class Median
{
    public static double Calculate(
        IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        var sortedValues = new List<double>();

        foreach (double value in values)
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All values must be finite.",
                    nameof(values));
            }

            sortedValues.Add(value);
        }

        if (sortedValues.Count == 0)
        {
            throw new ArgumentException(
                "The collection must contain at least one value.",
                nameof(values));
        }

        sortedValues.Sort();

        int middle = sortedValues.Count / 2;

        if (sortedValues.Count % 2 != 0)
        {
            return sortedValues[middle];
        }

        double lower = sortedValues[middle - 1];
        double upper = sortedValues[middle];

        return lower / 2.0 + upper / 2.0;
    }
}