namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class TimeSeriesSumTests
{
    [Fact]
    public void Calculate_WithPositiveValues_ReturnsSum()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(2), 30.0)
        ];

        Assert.Equal(60.0, TimeSeriesSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsSum()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, -10.0),
            new(start.AddHours(1), -20.0),
            new(start.AddHours(2), -30.0)
        ];

        Assert.Equal(-60.0, TimeSeriesSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithMixedValues_ReturnsSum()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, -10.0),
            new(start.AddHours(1), 5.0),
            new(start.AddHours(2), 5.0)
        ];

        Assert.Equal(0.0, TimeSeriesSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithUnsortedTimestamps_ReturnsSum()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start.AddHours(2), 30.0),
            new(start, 10.0),
            new(start.AddHours(1), 20.0)
        ];

        Assert.Equal(60.0, TimeSeriesSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithSingleObservation_ReturnsItsValue()
    {
        Observation[] observations =
        [
            new(
                new DateTimeOffset(
                    2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
                42.0)
        ];

        Assert.Equal(42.0, TimeSeriesSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithEmptySequence_ReturnsZero()
    {
        Assert.Equal(
            0.0,
            TimeSeriesSum.Calculate(Array.Empty<Observation>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimeSeriesSum.Calculate(null!));
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
            () => TimeSeriesSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithExtremePositiveValues_ThrowsOverflowException()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, double.MaxValue),
            new(start.AddMinutes(1), double.MaxValue)
        ];

        Assert.Throws<OverflowException>(
            () => TimeSeriesSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithExtremeNegativeValues_ThrowsOverflowException()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, -double.MaxValue),
            new(start.AddMinutes(1), -double.MaxValue)
        ];

        Assert.Throws<OverflowException>(
            () => TimeSeriesSum.Calculate(observations));
    }
}