namespace Invariant.Numerics.Tests.Statistics;

public sealed class MeanTests
{
    [Fact]
    public void Calculate_WithPositiveValues_ReturnsArithmeticMean()
    {
        double[] values = [2.0, 4.0, 6.0, 8.0];

        double result = Mean.Calculate(values);

        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Calculate_WithSingleValue_ReturnsThatValue()
    {
        double[] values = [42.0];

        double result = Mean.Calculate(values);

        Assert.Equal(42.0, result);
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsArithmeticMean()
    {
        double[] values = [-2.0, -4.0, -6.0];

        double result = Mean.Calculate(values);

        Assert.Equal(-4.0, result);
    }

    [Fact]
    public void Calculate_WithMixedValues_ReturnsArithmeticMean()
    {
        double[] values = [-10.0, 0.0, 10.0];

        double result = Mean.Calculate(values);

        Assert.Equal(0.0, result, 10);
    }

    [Fact]
    public void Calculate_WithFractionalValues_ReturnsArithmeticMean()
    {
        double[] values = [1.0, 2.0, 2.0];

        double result = Mean.Calculate(values);

        Assert.Equal(5.0 / 3.0, result, precision: 12);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => Mean.Calculate(Array.Empty<double>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => Mean.Calculate(null!));
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
            () => Mean.Calculate(values));
    }

    [Fact]
    public void Calculate_WithLargeFiniteValues_DoesNotOverflow()
    {
        double[] values =
        [
            double.MaxValue,
            double.MaxValue
        ];

        double result = Mean.Calculate(values);

        Assert.Equal(double.MaxValue, result);
    }

    [Fact]
    public void Calculate_WithOppositeExtremeValues_ReturnsZero()
    {
        double[] values = [double.MaxValue, -double.MaxValue];

        double result = Mean.Calculate(values);

        Assert.Equal(0.0, result);
    }
}