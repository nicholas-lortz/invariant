namespace Invariant.Numerics.Tests.Statistics;

public sealed class SumTests
{
    [Fact]
    public void Calculate_WithPositiveValues_ReturnsSum()
    {
        double[] values = [1.0, 2.0, 3.0, 4.0];

        double result = Sum.Calculate(values);

        Assert.Equal(10.0, result);
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsSum()
    {
        double[] values = [-1.0, -2.0, -3.0];

        double result = Sum.Calculate(values);

        Assert.Equal(-6.0, result);
    }

    [Fact]
    public void Calculate_WithMixedValues_ReturnsSum()
    {
        double[] values = [-10.0, 5.0, 5.0];

        double result = Sum.Calculate(values);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ReturnsZero()
    {
        double result = Sum.Calculate(Array.Empty<double>());

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithSingleValue_ReturnsThatValue()
    {
        double[] values = [42.0];

        double result = Sum.Calculate(values);

        Assert.Equal(42.0, result);
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => Sum.Calculate(null!));
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
            () => Sum.Calculate(values));
    }

    [Fact]
    public void Calculate_WhenSumOverflows_ThrowsOverflowException()
    {
        double[] values = [double.MaxValue, double.MaxValue];

        Assert.Throws<OverflowException>(
            () => Sum.Calculate(values));
    }
}