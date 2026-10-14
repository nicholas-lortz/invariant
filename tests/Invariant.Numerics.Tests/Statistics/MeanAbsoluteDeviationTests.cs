namespace Invariant.Numerics.Tests.Statistics;

public sealed class MeanAbsoluteDeviationTests
{
    [Fact]
    public void Calculate_WithValues_ReturnsMeanAbsoluteDeviation()
    {
        double[] values = [1.0, 2.0, 3.0, 4.0, 5.0];

        double result = MeanAbsoluteDeviation.Calculate(values);

        Assert.Equal(1.2, result, 10);
    }

    [Fact]
    public void Calculate_WithExplicitCenter_ReturnsMeanAbsoluteDeviation()
    {
        double[] values = [1.0, 2.0, 3.0];

        double result = MeanAbsoluteDeviation.Calculate(values, 2.0);

        Assert.Equal(2.0 / 3.0, result, 10);
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsMeanAbsoluteDeviation()
    {
        double[] values = [-5.0, -3.0, -1.0];

        double result = MeanAbsoluteDeviation.Calculate(values);

        Assert.Equal(4.0 / 3.0, result, 10);
    }

    [Fact]
    public void Calculate_WithIdenticalValues_ReturnsZero()
    {
        double[] values = [5.0, 5.0, 5.0];

        double result = MeanAbsoluteDeviation.Calculate(values);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithSingleValue_ReturnsZero()
    {
        double[] values = [42.0];

        double result = MeanAbsoluteDeviation.Calculate(values);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => MeanAbsoluteDeviation.Calculate(Array.Empty<double>()));
    }

    [Fact]
    public void Calculate_WithEmptySequenceAndExplicitCenter_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => MeanAbsoluteDeviation.Calculate(
                Array.Empty<double>(),
                0.0));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => MeanAbsoluteDeviation.Calculate(null!));
    }

    [Fact]
    public void Calculate_WithNullAndExplicitCenter_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => MeanAbsoluteDeviation.Calculate(null!, 0.0));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithNonFiniteValue_ThrowsArgumentException(
        double value)
    {
        Assert.Throws<ArgumentException>(
            () => MeanAbsoluteDeviation.Calculate([1.0, value, 3.0]));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithNonFiniteCenter_ThrowsArgumentOutOfRangeException(
        double center)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => MeanAbsoluteDeviation.Calculate([1.0, 2.0], center));
    }

    [Fact]
    public void Calculate_WithExtremeValues_ReturnsFiniteResult()
    {
        double[] values = [double.MaxValue, -double.MaxValue];

        double result = MeanAbsoluteDeviation.Calculate(values);

        Assert.Equal(double.MaxValue, result);
    }

    [Fact]
    public void Calculate_WithUnrepresentableDeviation_ThrowsOverflowException()
    {
        double[] values = [double.MaxValue];

        Assert.Throws<OverflowException>(
            () => MeanAbsoluteDeviation.Calculate(
                values,
                -double.MaxValue));
    }
}