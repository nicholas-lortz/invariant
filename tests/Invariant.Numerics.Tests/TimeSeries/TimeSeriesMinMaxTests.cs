namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class TimeSeriesMinMaxTests
{
    [Fact]
    public void Calculate_WithSequentialValues_ReturnsMinimumAndMaximum()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 5.0),
            new(start.AddHours(2), 20.0)
        ];

        TimeSeriesMinMaxResult result =
            TimeSeriesMinMax.Calculate(observations);

        Assert.Equal(5.0, result.Minimum.Value);
        Assert.Equal(start.AddHours(1), result.Minimum.Timestamp);

        Assert.Equal(20.0, result.Maximum.Value);
        Assert.Equal(start.AddHours(2), result.Maximum.Timestamp);
    }

    [Fact]
    public void Calculate_WithUnsortedValues_ReturnsCorrectExtrema()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 8.0),
            new(start.AddHours(2), 2.0),
            new(start.AddHours(1), 15.0)
        ];

        TimeSeriesMinMaxResult result =
            TimeSeriesMinMax.Calculate(observations);

        Assert.Equal(2.0, result.Minimum.Value);
        Assert.Equal(start.AddHours(2), result.Minimum.Timestamp);

        Assert.Equal(15.0, result.Maximum.Value);
        Assert.Equal(start.AddHours(1), result.Maximum.Timestamp);
    }

    [Fact]
    public void Calculate_WithIdenticalValues_ReturnsSameExtremes()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 42.0),
            new(start.AddHours(1), 42.0),
            new(start.AddHours(2), 42.0)
        ];

        TimeSeriesMinMaxResult result =
            TimeSeriesMinMax.Calculate(observations);

        Assert.Equal(42.0, result.Minimum.Value);
        Assert.Equal(start, result.Minimum.Timestamp);

        Assert.Equal(42.0, result.Maximum.Value);
        Assert.Equal(start, result.Maximum.Timestamp);
    }

    [Fact]
    public void Calculate_WithSingleObservation_ReturnsThatObservation()
    {
        Observation observation = new(
            new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero),
            7.0);

        TimeSeriesMinMaxResult result =
            TimeSeriesMinMax.Calculate([observation]);

        Assert.Equal(observation, result.Minimum);
        Assert.Equal(observation, result.Maximum);
    }

    [Fact]
    public void Calculate_WithExtremeFiniteValues_ReturnsCorrectExtremes()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, double.MaxValue),
            new(start.AddHours(1), -double.MaxValue),
            new(start.AddHours(2), 0.0)
        ];

        TimeSeriesMinMaxResult result =
            TimeSeriesMinMax.Calculate(observations);

        Assert.Equal(-double.MaxValue, result.Minimum.Value);
        Assert.Equal(double.MaxValue, result.Maximum.Value);
    }

    [Fact]
    public void Calculate_WithDuplicateExtrema_ReturnsFirstOccurrence()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 1.0),
            new(start.AddHours(1), 10.0),
            new(start.AddHours(2), 1.0),
            new(start.AddHours(3), 10.0)
        ];

        TimeSeriesMinMaxResult result =
            TimeSeriesMinMax.Calculate(observations);

        Assert.Equal(start, result.Minimum.Timestamp);
        Assert.Equal(start.AddHours(1), result.Maximum.Timestamp);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => TimeSeriesMinMax.Calculate(
                Array.Empty<Observation>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimeSeriesMinMax.Calculate(null!));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithNonFiniteValue_ThrowsArgumentException(
        double value)
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 1.0),
            new(start.AddHours(1), value)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeSeriesMinMax.Calculate(observations));
    }
}