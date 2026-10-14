namespace Invariant.Numerics.Statistics;

public static class Product
{
    public static double Calculate(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        double product = 1.0;

        foreach (double value in values)
        {
            if (!double.IsFinite(value))
            {
                throw new ArgumentException(
                    "All values must be finite.",
                    nameof(values));
            }

            product *= value;

            if (!double.IsFinite(product))
            {
                throw new OverflowException(
                    "The product exceeds the representable finite double range.");
            }
        }

        return product;
    }
}