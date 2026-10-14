namespace Invariant.Numerics.Tests.Statistics;

public sealed class InterquartileRangeTests
{
    [Fact]
    public void Calculate_WithSequentialValues_ReturnsInterquartileRange()
    {
        double[] values = [1.0, 2.0, 3.0, 4.0, 5.0];

        double result = InterquartileRange.Calculate(values);

        Assert.Equal(2.0, result);
    }

    [Fact]
    public void Calculate_WithEvenNumberOfValues_ReturnsInterquartileRange()
    {
        double[] values = [1.0, 2.0, 3.0, 4.0];

        double result = InterquartileRange.Calculate(values);

        Assert.Equal(1.5, result);
    }

    [Fact]
    public void Calculate_WithIdenticalValues_ReturnsZero()
    {
        double[] values = [5.0, 5.0, 5.0, 5.0];

        double result = InterquartileRange.Calculate(values);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithUnsortedValues_ReturnsInterquartileRange()
    {
        double[] values = [9.0, 1.0, 5.0, 3.0, 7.0];

        double result = InterquartileRange.Calculate(values);

        Assert.Equal(4.0, result);
    }

    [Fact]
    public void Calculate_WithSingleValue_ReturnsZero()
    {
        double[] values = [42.0];

        double result = InterquartileRange.Calculate(values);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => InterquartileRange.Calculate(Array.Empty<double>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => InterquartileRange.Calculate(null!));
    }

    [Fact]
    public void Calculate_WithExtremeValues_ThrowsOverflowException()
    {
        double[] values =
        [
            -double.MaxValue,
        -double.MaxValue,
        double.MaxValue,
        double.MaxValue
        ];

        Assert.Throws<OverflowException>(
            () => InterquartileRange.Calculate(values));
    }
}