namespace Invariant.Numerics.Tests.Statistics;

public sealed class DistinctCountTests
{
    [Fact]
    public void Calculate_WithUniqueValues_ReturnsCount()
    {
        double result = DistinctCount.Calculate([1.0, 2.0, 3.0]);

        Assert.Equal(3L, (long)result);
    }

    [Fact]
    public void Calculate_WithDuplicateValues_ReturnsDistinctCount()
    {
        long result = DistinctCount.Calculate([1.0, 2.0, 2.0, 3.0, 3.0]);

        Assert.Equal(3L, result);
    }

    [Fact]
    public void Calculate_WithIdenticalValues_ReturnsOne()
    {
        long result = DistinctCount.Calculate([5.0, 5.0, 5.0]);

        Assert.Equal(1L, result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ReturnsZero()
    {
        long result = DistinctCount.Calculate(Array.Empty<double>());

        Assert.Equal(0L, result);
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => DistinctCount.Calculate(null!));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithNonFiniteValue_ThrowsArgumentException(
        double value)
    {
        Assert.Throws<ArgumentException>(
            () => DistinctCount.Calculate([1.0, value, 3.0]));
    }

    [Fact]
    public void Calculate_WithPositiveAndNegativeZero_ReturnsOne()
    {
        long result = DistinctCount.Calculate([0.0, -0.0]);

        Assert.Equal(1L, result);
    }

    [Fact]
    public void Calculate_WithExtremeFiniteValues_ReturnsDistinctCount()
    {
        long result = DistinctCount.Calculate(
            [double.MaxValue, double.MinValue, double.MaxValue]);

        Assert.Equal(2L, result);
    }
}