namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class TimeSeriesMedianTests
{
    [Fact]
    public void Calculate_WithOddNumberOfValues_ReturnsMedian()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 5.0),
            new(start.AddHours(1), 1.0),
            new(start.AddHours(2), 3.0)
        ];

        double result = TimeSeriesMedian.Calculate(observations);

        Assert.Equal(3.0, result);
    }

    [Fact]
    public void Calculate_WithEvenNumberOfValues_ReturnsMedian()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 4.0),
            new(start.AddHours(1), 1.0),
            new(start.AddHours(2), 3.0),
            new(start.AddHours(3), 2.0)
        ];

        double result = TimeSeriesMedian.Calculate(observations);

        Assert.Equal(2.5, result);
    }

    [Fact]
    public void Calculate_WithUnsortedObservations_ReturnsMedian()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 9.0),
            new(start.AddHours(1), 1.0),
            new(start.AddHours(2), 5.0),
            new(start.AddHours(3), 3.0),
            new(start.AddHours(4), 7.0)
        ];

        double result = TimeSeriesMedian.Calculate(observations);

        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Calculate_WithIdenticalValues_ReturnsThatValue()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 42.0),
            new(start.AddHours(1), 42.0),
            new(start.AddHours(2), 42.0)
        ];

        double result = TimeSeriesMedian.Calculate(observations);

        Assert.Equal(42.0, result);
    }

    [Fact]
    public void Calculate_WithSingleObservation_ReturnsItsValue()
    {
        Observation[] observations =
        [
            new(
                new DateTimeOffset(
                    2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
                17.0)
        ];

        double result = TimeSeriesMedian.Calculate(observations);

        Assert.Equal(17.0, result);
    }

    [Fact]
    public void Calculate_WithExtremeValues_ReturnsFiniteMedian()
    {
        Observation[] observations =
        [
            new(DateTimeOffset.MinValue, -double.MaxValue),
            new(DateTimeOffset.MaxValue, double.MaxValue)
        ];

        double result = TimeSeriesMedian.Calculate(observations);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => TimeSeriesMedian.Calculate(
                Array.Empty<Observation>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimeSeriesMedian.Calculate(null!));
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
            () => TimeSeriesMedian.Calculate(observations));
    }
}