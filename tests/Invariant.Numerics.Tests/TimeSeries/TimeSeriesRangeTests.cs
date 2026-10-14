namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class TimeSeriesRangeTests
{
    [Fact]
    public void Calculate_WithIncreasingValues_ReturnsRange()
    {
        DateTimeOffset start = new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(2), 30.0)
        ];

        double result = TimeSeriesRange.Calculate(observations);

        Assert.Equal(20.0, result);
    }

    [Fact]
    public void Calculate_WithMixedPositiveAndNegativeValues_ReturnsRange()
    {
        DateTimeOffset start = new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, -10.0),
            new(start.AddHours(1), 5.0),
            new(start.AddHours(2), 20.0)
        ];

        double result = TimeSeriesRange.Calculate(observations);

        Assert.Equal(30.0, result);
    }

    [Fact]
    public void Calculate_WithIdenticalValues_ReturnsZero()
    {
        DateTimeOffset start = new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 5.0),
            new(start.AddHours(1), 5.0),
            new(start.AddHours(2), 5.0)
        ];

        double result = TimeSeriesRange.Calculate(observations);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithSingleObservation_ReturnsZero()
    {
        Observation[] observations =
        [
            new(DateTimeOffset.UtcNow, 42.0)
        ];

        double result = TimeSeriesRange.Calculate(observations);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => TimeSeriesRange.Calculate(
                Array.Empty<Observation>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimeSeriesRange.Calculate(null!));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithNonFiniteValue_ThrowsArgumentException(
        double value)
    {
        Observation[] observations =
        [
            new(DateTimeOffset.UtcNow, value)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeSeriesRange.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithExtremeValues_ThrowsOverflowException()
    {
        DateTimeOffset start = new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, -double.MaxValue),
            new(start.AddHours(1), double.MaxValue)
        ];

        Assert.Throws<OverflowException>(
            () => TimeSeriesRange.Calculate(observations));
    }
}