namespace Invariant.Numerics.Tests.Statistics;

public sealed class MaximumTests
{
    [Fact]
    public void Calculate_WithPositiveValues_ReturnsMaximum()
    {
        double[] values = [3.0, 1.0, 5.0, 2.0];

        double result = Maximum.Calculate(values);

        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsMaximum()
    {
        double[] values = [-3.0, -1.0, -5.0, -2.0];

        double result = Maximum.Calculate(values);

        Assert.Equal(-1.0, result);
    }

    [Fact]
    public void Calculate_WithMixedValues_ReturnsMaximum()
    {
        double[] values = [-3.0, 0.0, 5.0];

        double result = Maximum.Calculate(values);

        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Calculate_WithSingleValue_ReturnsThatValue()
    {
        double[] values = [42.0];

        double result = Maximum.Calculate(values);

        Assert.Equal(42.0, result);
    }

    [Fact]
    public void Calculate_WithDuplicateMaximum_ReturnsMaximum()
    {
        double[] values = [2.0, 5.0, 5.0, 3.0];

        double result = Maximum.Calculate(values);

        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => Maximum.Calculate(Array.Empty<double>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => Maximum.Calculate(null!));
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
            () => Maximum.Calculate(values));
    }

    [Fact]
    public void Calculate_WithExtremeFiniteValues_ReturnsMaximum()
    {
        double[] values = [double.MaxValue, double.MinValue];

        double result = Maximum.Calculate(values);

        Assert.Equal(double.MaxValue, result);
    }
}