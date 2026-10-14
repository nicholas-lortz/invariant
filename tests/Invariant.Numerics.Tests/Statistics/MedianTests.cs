namespace Invariant.Numerics.Tests.Statistics;

public sealed class MedianTests
{
    [Fact]
    public void Calculate_WithOddNumberOfValues_ReturnsMiddleValue()
    {
        double[] values = [1.0, 3.0, 5.0];

        double result = Median.Calculate(values);

        Assert.Equal(3.0, result);
    }

    [Fact]
    public void Calculate_WithEvenNumberOfValues_ReturnsMeanOfMiddleValues()
    {
        double[] values = [1.0, 3.0, 5.0, 7.0];

        double result = Median.Calculate(values);

        Assert.Equal(4.0, result);
    }

    [Fact]
    public void Calculate_WithUnsortedValues_ReturnsMedian()
    {
        double[] values = [9.0, 1.0, 5.0, 3.0, 7.0];

        double result = Median.Calculate(values);

        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Calculate_WithSingleValue_ReturnsThatValue()
    {
        double[] values = [42.0];

        double result = Median.Calculate(values);

        Assert.Equal(42.0, result);
    }

    [Fact]
    public void Calculate_WithDuplicateValues_ReturnsMedian()
    {
        double[] values = [2.0, 2.0, 2.0, 8.0];

        double result = Median.Calculate(values);

        Assert.Equal(2.0, result);
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsMedian()
    {
        double[] values = [-9.0, -5.0, -1.0];

        double result = Median.Calculate(values);

        Assert.Equal(-5.0, result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => Median.Calculate(Array.Empty<double>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => Median.Calculate(null!));
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
            () => Median.Calculate(values));
    }

    [Fact]
    public void Calculate_WithExtremeFiniteValues_ReturnsFiniteMedian()
    {
        double[] values =
        [
            double.MaxValue,
            double.MaxValue
        ];

        double result = Median.Calculate(values);

        Assert.Equal(double.MaxValue, result);
    }
}