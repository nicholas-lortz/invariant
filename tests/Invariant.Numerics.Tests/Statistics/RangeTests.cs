namespace Invariant.Numerics.Tests.Statistics;

public sealed class RangeTests
{
    [Fact]
    public void Calculate_WithPositiveValues_ReturnsRange()
    {
        double[] values = [3.0, 1.0, 5.0, 2.0];

        double result = Invariant.Numerics.Statistics.Range.Calculate(values);

        Assert.Equal(4.0, result);
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsRange()
    {
        double[] values = [-8.0, -3.0, -5.0];

        double result = Invariant.Numerics.Statistics.Range.Calculate(values);

        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Calculate_WithMixedValues_ReturnsRange()
    {
        double[] values = [-5.0, 0.0, 10.0];

        double result = Invariant.Numerics.Statistics.Range.Calculate(values);

        Assert.Equal(15.0, result);
    }

    [Fact]
    public void Calculate_WithIdenticalValues_ReturnsZero()
    {
        double[] values = [5.0, 5.0, 5.0];

        double result = Invariant.Numerics.Statistics.Range.Calculate(values);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithSingleValue_ReturnsZero()
    {
        double[] values = [42.0];

        double result = Invariant.Numerics.Statistics.Range.Calculate(values);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => Invariant.Numerics.Statistics.Range.Calculate(Array.Empty<double>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => Invariant.Numerics.Statistics.Range.Calculate(null!));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithNonFiniteValue_ThrowsArgumentException(
        double value)
    {
        double[] values = [1.0, value, 3.0];

        Assert.Throws<ArgumentException>(
            () => Invariant.Numerics.Statistics.Range.Calculate(values));
    }

    [Fact]
    public void Calculate_WithExtremeValues_ThrowsOverflowException()
    {
        double[] values = [-double.MaxValue, double.MaxValue];

        Assert.Throws<OverflowException>(
            () => Invariant.Numerics.Statistics.Range.Calculate(values));
    }
}