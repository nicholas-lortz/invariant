namespace Invariant.Numerics.Tests.Statistics;

public sealed class HarmonicMeanTests
{
    [Fact]
    public void Calculate_WithPositiveValues_ReturnsHarmonicMean()
    {
        double result = HarmonicMean.Calculate([2.0, 4.0]);

        Assert.Equal(8.0 / 3.0, result, 10);
    }

    [Fact]
    public void Calculate_WithIdenticalValues_ReturnsValue()
    {
        double result = HarmonicMean.Calculate([5.0, 5.0, 5.0]);

        Assert.Equal(5.0, result, 10);
    }

    [Fact]
    public void Calculate_WithSingleValue_ReturnsThatValue()
    {
        double result = HarmonicMean.Calculate([42.0]);

        Assert.Equal(42.0, result, 10);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => HarmonicMean.Calculate(Array.Empty<double>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => HarmonicMean.Calculate(null!));
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(-1.0)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithInvalidValue_ThrowsArgumentException(
        double value)
    {
        Assert.Throws<ArgumentException>(
            () => HarmonicMean.Calculate([1.0, value, 4.0]));
    }

    [Fact]
    public void Calculate_WithExtremePositiveValues_ReturnsFiniteResult()
    {
        double result = HarmonicMean.Calculate(
            [double.MaxValue, double.MaxValue]);

        Assert.Equal(double.MaxValue, result);
    }
}