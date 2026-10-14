namespace Invariant.Numerics.Tests.Statistics;

public sealed class WeightedMeanTests
{
    [Fact]
    public void Calculate_WithEqualWeights_ReturnsArithmeticMean()
    {
        double result = WeightedMean.Calculate(
            [2.0, 4.0, 6.0],
            [1.0, 1.0, 1.0]);

        Assert.Equal(4.0, result, 10);
    }

    [Fact]
    public void Calculate_WithUnequalWeights_ReturnsWeightedMean()
    {
        double result = WeightedMean.Calculate(
            [10.0, 20.0],
            [1.0, 3.0]);

        Assert.Equal(17.5, result, 10);
    }

    [Fact]
    public void Calculate_WithZeroWeight_IgnoresCorrespondingValue()
    {
        double result = WeightedMean.Calculate(
            [10.0, 100.0],
            [1.0, 0.0]);

        Assert.Equal(10.0, result, 10);
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsWeightedMean()
    {
        double result = WeightedMean.Calculate(
            [-10.0, 10.0],
            [1.0, 1.0]);

        Assert.Equal(0.0, result, 10);
    }

    [Fact]
    public void Calculate_WithEmptySequences_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => WeightedMean.Calculate(
                Array.Empty<double>(),
                Array.Empty<double>()));
    }

    [Fact]
    public void Calculate_WithMismatchedLengths_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => WeightedMean.Calculate(
                [1.0, 2.0],
                [1.0]));
    }

    [Fact]
    public void Calculate_WithAllZeroWeights_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => WeightedMean.Calculate(
                [1.0, 2.0],
                [0.0, 0.0]));
    }

    [Fact]
    public void Calculate_WithNullValues_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => WeightedMean.Calculate(null!, [1.0]));
    }

    [Fact]
    public void Calculate_WithNullWeights_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => WeightedMean.Calculate([1.0], null!));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithNonFiniteValue_ThrowsArgumentException(
        double value)
    {
        Assert.Throws<ArgumentException>(
            () => WeightedMean.Calculate([value], [1.0]));
    }

    [Theory]
    [InlineData(-1.0)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithInvalidWeight_ThrowsArgumentException(
        double weight)
    {
        Assert.Throws<ArgumentException>(
            () => WeightedMean.Calculate([1.0], [weight]));
    }

    [Fact]
    public void Calculate_WithExtremeValues_ReturnsFiniteMean()
    {
        double result = WeightedMean.Calculate(
            [double.MaxValue, -double.MaxValue],
            [1.0, 1.0]);

        Assert.Equal(0.0, result, 10);
    }
}