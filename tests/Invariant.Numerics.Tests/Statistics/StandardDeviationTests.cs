namespace Invariant.Numerics.Tests.Statistics;

public sealed class StandardDeviationTests
{
    [Fact]
    public void CalculatePopulation_WithValues_ReturnsStandardDeviation()
    {
        double[] values = [2.0, 4.0, 6.0];

        double result = StandardDeviation.CalculatePopulation(values);

        Assert.Equal(Math.Sqrt(8.0 / 3.0), result, precision: 12);
    }

    [Fact]
    public void CalculateSample_WithValues_ReturnsStandardDeviation()
    {
        double[] values = [2.0, 4.0, 6.0];

        double result = StandardDeviation.CalculateSample(values);

        Assert.Equal(2.0, result);
    }

    [Fact]
    public void CalculatePopulation_WithIdenticalValues_ReturnsZero()
    {
        double[] values = [5.0, 5.0, 5.0];

        double result = StandardDeviation.CalculatePopulation(values);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void CalculateSample_WithIdenticalValues_ReturnsZero()
    {
        double[] values = [5.0, 5.0, 5.0];

        double result = StandardDeviation.CalculateSample(values);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void CalculatePopulation_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => StandardDeviation.CalculatePopulation(
                Array.Empty<double>()));
    }

    [Fact]
    public void CalculateSample_WithSingleValue_ThrowsArgumentException()
    {
        double[] values = [42.0];

        Assert.Throws<ArgumentException>(
            () => StandardDeviation.CalculateSample(values));
    }

    [Fact]
    public void CalculatePopulation_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => StandardDeviation.CalculatePopulation(null!));
    }

    [Fact]
    public void CalculateSample_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => StandardDeviation.CalculateSample(null!));
    }
}