namespace Invariant.Numerics.Tests.Statistics;

public sealed class VarianceTests
{
    [Fact]
    public void CalculatePopulation_WithPositiveValues_ReturnsVariance()
    {
        double[] values = [2.0, 4.0, 6.0];

        double result = Variance.CalculatePopulation(values);

        Assert.Equal(8.0 / 3.0, result, precision: 12);
    }

    [Fact]
    public void CalculateSample_WithPositiveValues_ReturnsVariance()
    {
        double[] values = [2.0, 4.0, 6.0];

        double result = Variance.CalculateSample(values);

        Assert.Equal(4.0, result);
    }

    [Fact]
    public void CalculatePopulation_WithIdenticalValues_ReturnsZero()
    {
        double[] values = [5.0, 5.0, 5.0];

        double result = Variance.CalculatePopulation(values);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void CalculateSample_WithIdenticalValues_ReturnsZero()
    {
        double[] values = [5.0, 5.0, 5.0];

        double result = Variance.CalculateSample(values);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void CalculatePopulation_WithSequentialValues_ReturnsVariance()
    {
        double[] values = [1.0, 2.0, 3.0, 4.0, 5.0];

        double result = Variance.CalculatePopulation(values);

        Assert.Equal(2.0, result);
    }

    [Fact]
    public void CalculateSample_WithSequentialValues_ReturnsVariance()
    {
        double[] values = [1.0, 2.0, 3.0, 4.0, 5.0];

        double result = Variance.CalculateSample(values);

        Assert.Equal(2.5, result);
    }

    [Fact]
    public void CalculatePopulation_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => Variance.CalculatePopulation(Array.Empty<double>()));
    }

    [Fact]
    public void CalculateSample_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => Variance.CalculateSample(Array.Empty<double>()));
    }

    [Fact]
    public void CalculateSample_WithSingleValue_ThrowsArgumentException()
    {
        double[] values = [42.0];

        Assert.Throws<ArgumentException>(
            () => Variance.CalculateSample(values));
    }

    [Fact]
    public void CalculatePopulation_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => Variance.CalculatePopulation(null!));
    }

    [Fact]
    public void CalculateSample_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => Variance.CalculateSample(null!));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void CalculatePopulation_WithNonFiniteValue_ThrowsArgumentException(
        double value)
    {
        double[] values = [1.0, value, 3.0];

        Assert.Throws<ArgumentException>(
            () => Variance.CalculatePopulation(values));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void CalculateSample_WithNonFiniteValue_ThrowsArgumentException(
        double value)
    {
        double[] values = [1.0, value, 3.0];

        Assert.Throws<ArgumentException>(
            () => Variance.CalculateSample(values));
    }

    [Fact]
    public void CalculatePopulation_WithNegativeValues_ReturnsVariance()
    {
        double[] values = [-6.0, -4.0, -2.0];

        double result = Variance.CalculatePopulation(values);

        Assert.Equal(8.0 / 3.0, result, precision: 12);
    }
}