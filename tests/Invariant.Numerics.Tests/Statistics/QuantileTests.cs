namespace Invariant.Numerics.Tests.Statistics;

public sealed class QuantileTests
{
    [Fact]
    public void Calculate_WithProbabilityZero_ReturnsMinimum()
    {
        double[] values = [3.0, 1.0, 5.0, 2.0];

        double result = Quantile.Calculate(values, 0.0);

        Assert.Equal(1.0, result);
    }

    [Fact]
    public void Calculate_WithProbabilityOne_ReturnsMaximum()
    {
        double[] values = [3.0, 1.0, 5.0, 2.0];

        double result = Quantile.Calculate(values, 1.0);

        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Calculate_WithMedianProbability_ReturnsMedian()
    {
        double[] values = [1.0, 3.0, 5.0, 7.0];

        double result = Quantile.Calculate(values, 0.5);

        Assert.Equal(4.0, result);
    }

    [Fact]
    public void Calculate_WithUnsortedValues_ReturnsQuantile()
    {
        double[] values = [9.0, 1.0, 5.0, 3.0, 7.0];

        double result = Quantile.Calculate(values, 0.5);

        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Calculate_WithInterpolation_ReturnsInterpolatedValue()
    {
        double[] values = [0.0, 10.0];

        double result = Quantile.Calculate(values, 0.25);

        Assert.Equal(2.5, result);
    }

    [Fact]
    public void Calculate_WithSingleValue_ReturnsThatValue()
    {
        double[] values = [42.0];

        double result = Quantile.Calculate(values, 0.75);

        Assert.Equal(42.0, result);
    }

    [Fact]
    public void Calculate_WithDuplicateValues_ReturnsQuantile()
    {
        double[] values = [2.0, 2.0, 2.0, 8.0];

        double result = Quantile.Calculate(values, 0.5);

        Assert.Equal(2.0, result);
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsQuantile()
    {
        double[] values = [-9.0, -5.0, -1.0];

        double result = Quantile.Calculate(values, 0.5);

        Assert.Equal(-5.0, result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => Quantile.Calculate(Array.Empty<double>(), 0.5));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => Quantile.Calculate(null!, 0.5));
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(1.1)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithInvalidProbability_ThrowsArgumentOutOfRangeException(
        double probability)
    {
        double[] values = [1.0, 2.0, 3.0];

        Assert.Throws<ArgumentOutOfRangeException>(
            () => Quantile.Calculate(values, probability));
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
            () => Quantile.Calculate(values, 0.5));
    }

    [Fact]
    public void Calculate_WithExtremeFiniteValues_ReturnsFiniteQuantile()
    {
        double[] values = [double.MaxValue, double.MaxValue];

        double result = Quantile.Calculate(values, 0.5);

        Assert.Equal(double.MaxValue, result);
    }
}