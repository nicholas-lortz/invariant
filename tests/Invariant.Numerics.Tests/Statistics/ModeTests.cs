namespace Invariant.Numerics.Tests.Statistics;

public sealed class ModeTests
{
    [Fact]
    public void Calculate_WithOneMode_ReturnsMostFrequentValue()
    {
        double[] values = [1.0, 2.0, 2.0, 3.0];

        var result = Mode.Calculate(values);

        Assert.Equal(new[] { 2.0 }, result);
    }

    [Fact]
    public void Calculate_WithTwoModes_ReturnsBothModes()
    {
        double[] values = [1.0, 1.0, 2.0, 2.0, 3.0];

        var result = Mode.Calculate(values);

        Assert.Equal(new[] { 1.0, 2.0 }, result);
    }

    [Fact]
    public void Calculate_WithAllUniqueValues_ReturnsAllValues()
    {
        double[] values = [3.0, 1.0, 2.0];

        var result = Mode.Calculate(values);

        Assert.Equal(new[] { 1.0, 2.0, 3.0 }, result);
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsMode()
    {
        double[] values = [-2.0, -2.0, -1.0, 0.0];

        var result = Mode.Calculate(values);

        Assert.Equal(new[] { -2.0 }, result);
    }

    [Fact]
    public void Calculate_WithSingleValue_ReturnsThatValue()
    {
        double[] values = [42.0];

        var result = Mode.Calculate(values);

        Assert.Equal(new[] { 42.0 }, result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => Mode.Calculate(Array.Empty<double>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => Mode.Calculate(null!));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithNonFiniteValue_ThrowsArgumentException(
        double value)
    {
        double[] values = [1.0, value, 2.0];

        Assert.Throws<ArgumentException>(
            () => Mode.Calculate(values));
    }
}