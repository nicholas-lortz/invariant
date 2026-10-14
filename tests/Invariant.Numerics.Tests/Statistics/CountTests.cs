namespace Invariant.Numerics.Tests.Statistics;

public sealed class CountTests
{
    [Fact]
    public void Calculate_WithValues_ReturnsObservationCount()
    {
        double[] values = [1.0, 2.0, 2.0, 4.0];

        long result = Count.Calculate(values);

        Assert.Equal(4L, result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ReturnsZero()
    {
        long result = Count.Calculate(Array.Empty<double>());

        Assert.Equal(0L, result);
    }

    [Fact]
    public void Calculate_WithSingleValue_ReturnsOne()
    {
        double[] values = [42.0];

        long result = Count.Calculate(values);

        Assert.Equal(1L, result);
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => Count.Calculate(null!));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithNonFiniteValue_ThrowsArgumentException(
        double value)
    {
        Assert.Throws<ArgumentException>(
            () => Count.Calculate([1.0, value, 3.0]));
    }

    [Fact]
    public void Calculate_WithExtremeFiniteValues_ReturnsCount()
    {
        double[] values = [double.MaxValue, double.MinValue];

        long result = Count.Calculate(values);

        Assert.Equal(2L, result);
    }
}