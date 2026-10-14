namespace Invariant.Numerics.Tests.Statistics;

public sealed class MedianAbsoluteDeviationTests
{
    [Fact]
    public void Calculate_WithValues_ReturnsMedianAbsoluteDeviation()
    {
        double result = MedianAbsoluteDeviation.Calculate(
            [1.0, 2.0, 3.0, 4.0, 5.0]);

        Assert.Equal(1.0, result);
    }

    [Fact]
    public void Calculate_WithUnsortedValues_ReturnsMedianAbsoluteDeviation()
    {
        double result = MedianAbsoluteDeviation.Calculate(
            [9.0, 1.0, 5.0, 3.0, 7.0]);

        Assert.Equal(2.0, result);
    }

    [Fact]
    public void Calculate_WithIdenticalValues_ReturnsZero()
    {
        double result = MedianAbsoluteDeviation.Calculate(
            [5.0, 5.0, 5.0]);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithSingleValue_ReturnsZero()
    {
        double result = MedianAbsoluteDeviation.Calculate([42.0]);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => MedianAbsoluteDeviation.Calculate(Array.Empty<double>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => MedianAbsoluteDeviation.Calculate(null!));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithNonFiniteValue_ThrowsArgumentException(
        double value)
    {
        Assert.Throws<ArgumentException>(
            () => MedianAbsoluteDeviation.Calculate([1.0, value, 3.0]));
    }

    [Fact]
    public void Calculate_WithExtremeValues_ReturnsFiniteResult()
    {
        double result = MedianAbsoluteDeviation.Calculate(
            [-double.MaxValue, double.MaxValue]);

        Assert.Equal(double.MaxValue, result);
    }
}