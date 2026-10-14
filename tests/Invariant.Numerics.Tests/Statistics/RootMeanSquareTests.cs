namespace Invariant.Numerics.Tests.Statistics;

public sealed class RootMeanSquareTests
{
    [Fact]
    public void Calculate_WithPositiveValues_ReturnsRootMeanSquare()
    {
        double[] values = [3.0, 4.0];

        double result = RootMeanSquare.Calculate(values);

        Assert.Equal(Math.Sqrt(12.5), result, 10);
    }

    [Fact]
    public void Calculate_WithMixedValues_ReturnsRootMeanSquare()
    {
        double[] values = [-3.0, 0.0, 3.0];

        double result = RootMeanSquare.Calculate(values);

        Assert.Equal(Math.Sqrt(6.0), result, 10);
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsPositiveRootMeanSquare()
    {
        double[] values = [-3.0, -4.0];

        double result = RootMeanSquare.Calculate(values);

        Assert.Equal(Math.Sqrt(12.5), result, 10);
    }

    [Fact]
    public void Calculate_WithSingleValue_ReturnsAbsoluteValue()
    {
        double[] values = [-42.0];

        double result = RootMeanSquare.Calculate(values);

        Assert.Equal(42.0, result);
    }

    [Fact]
    public void Calculate_WithZeros_ReturnsZero()
    {
        double[] values = [0.0, 0.0, 0.0];

        double result = RootMeanSquare.Calculate(values);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithUnsortedValues_ReturnsRootMeanSquare()
    {
        double[] values = [4.0, 3.0, 0.0];

        double result = RootMeanSquare.Calculate(values);

        Assert.Equal(Math.Sqrt(25.0 / 3.0), result, 10);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => RootMeanSquare.Calculate(Array.Empty<double>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => RootMeanSquare.Calculate(null!));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithNonFiniteValue_ThrowsArgumentException(
        double value)
    {
        Assert.Throws<ArgumentException>(
            () => RootMeanSquare.Calculate([1.0, value, 3.0]));
    }

    [Fact]
    public void Calculate_WithExtremeFiniteValues_ReturnsFiniteResult()
    {
        double[] values = [double.MaxValue, double.MaxValue];

        double result = RootMeanSquare.Calculate(values);

        Assert.Equal(double.MaxValue, result);
    }

    [Fact]
    public void Calculate_WithOppositeExtremeValues_ReturnsFiniteResult()
    {
        double[] values = [double.MaxValue, -double.MaxValue];

        double result = RootMeanSquare.Calculate(values);

        Assert.Equal(double.MaxValue, result);
    }
}