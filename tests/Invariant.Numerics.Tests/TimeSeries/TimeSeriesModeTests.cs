namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class TimeSeriesModeTests
{
    [Fact]
    public void Calculate_WithRepeatedValues_ReturnsMode()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddMinutes(1), 20.0),
            new(start.AddMinutes(2), 10.0),
            new(start.AddMinutes(3), 30.0),
            new(start.AddMinutes(4), 10.0)
        ];

        Assert.Equal(
            new[] { 10.0 },
            TimeSeriesMode.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithMultipleModes_ReturnsAllModesInAscendingOrder()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 30.0),
            new(start.AddMinutes(1), 10.0),
            new(start.AddMinutes(2), 20.0),
            new(start.AddMinutes(3), 10.0),
            new(start.AddMinutes(4), 30.0),
            new(start.AddMinutes(5), 20.0)
        ];

        Assert.Equal(
            new[] { 10.0, 20.0, 30.0 },
            TimeSeriesMode.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithNoRepeatedValues_ReturnsAllValues()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 30.0),
            new(start.AddMinutes(1), 10.0),
            new(start.AddMinutes(2), 20.0)
        ];

        Assert.Equal(
            new[] { 10.0, 20.0, 30.0 },
            TimeSeriesMode.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithUnsortedTimestamps_ReturnsMode()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start.AddMinutes(2), 5.0),
            new(start, 10.0),
            new(start.AddMinutes(1), 5.0)
        ];

        Assert.Equal(
            new[] { 5.0 },
            TimeSeriesMode.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsMode()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, -5.0),
            new(start.AddMinutes(1), -2.0),
            new(start.AddMinutes(2), -5.0)
        ];

        Assert.Equal(
            new[] { -5.0 },
            TimeSeriesMode.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithIdenticalValues_ReturnsSingleMode()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 42.0),
            new(start.AddMinutes(1), 42.0),
            new(start.AddMinutes(2), 42.0)
        ];

        Assert.Equal(
            new[] { 42.0 },
            TimeSeriesMode.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => TimeSeriesMode.Calculate(
                Array.Empty<Observation>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimeSeriesMode.Calculate(null!));
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
            new(
                new DateTimeOffset(
                    2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
                value)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeSeriesMode.Calculate(observations));
    }
}