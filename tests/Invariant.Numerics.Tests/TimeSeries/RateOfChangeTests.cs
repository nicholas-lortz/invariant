namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class RateOfChangeTests
{
    [Fact]
    public void Calculate_WithIncreasingValues_ReturnsRatePerSecond()
    {
        DateTime start = new(2026, 1, 1);

        Observation first = new(start, 10.0);
        Observation second = new(start.AddSeconds(5), 20.0);

        double result = RateOfChange.Calculate(first, second);

        Assert.Equal(2.0, result);
    }

    [Fact]
    public void Calculate_WithDecreasingValues_ReturnsNegativeRate()
    {
        DateTime start = new(2026, 1, 1);

        Observation first = new(start, 20.0);
        Observation second = new(start.AddSeconds(4), 8.0);

        double result = RateOfChange.Calculate(first, second);

        Assert.Equal(-3.0, result);
    }

    [Fact]
    public void Calculate_WithUnchangedValues_ReturnsZero()
    {
        DateTime start = new(2026, 1, 1);

        Observation first = new(start, 42.0);
        Observation second = new(start.AddMinutes(1), 42.0);

        double result = RateOfChange.Calculate(first, second);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithFractionalElapsedSeconds_ReturnsRate()
    {
        DateTime start = new(2026, 1, 1);

        Observation first = new(start, 0.0);
        Observation second = new(start.AddMilliseconds(500), 10.0);

        double result = RateOfChange.Calculate(first, second);

        Assert.Equal(20.0, result);
    }

    [Fact]
    public void Calculate_WithEndTimestampBeforeStart_ThrowsArgumentException()
    {
        DateTime start = new(2026, 1, 1);

        Observation first = new(start, 10.0);
        Observation second = new(start.AddSeconds(-1), 20.0);

        Assert.Throws<ArgumentException>(
            () => RateOfChange.Calculate(first, second));
    }

    [Fact]
    public void Calculate_WithIdenticalTimestamps_ThrowsArgumentException()
    {
        DateTime timestamp = new(2026, 1, 1);

        Observation first = new(timestamp, 10.0);
        Observation second = new(timestamp, 20.0);

        Assert.Throws<ArgumentException>(
            () => RateOfChange.Calculate(first, second));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithNonFiniteStartValue_ThrowsArgumentException(
        double value)
    {
        DateTime start = new(2026, 1, 1);

        Observation first = new(start, value);
        Observation second = new(start.AddSeconds(1), 20.0);

        Assert.Throws<ArgumentException>(
            () => RateOfChange.Calculate(first, second));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithNonFiniteEndValue_ThrowsArgumentException(
        double value)
    {
        DateTime start = new(2026, 1, 1);

        Observation first = new(start, 10.0);
        Observation second = new(start.AddSeconds(1), value);

        Assert.Throws<ArgumentException>(
            () => RateOfChange.Calculate(first, second));
    }
}