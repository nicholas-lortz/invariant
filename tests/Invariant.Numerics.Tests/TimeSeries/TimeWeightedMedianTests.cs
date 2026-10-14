namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class TimeWeightedMedianTests
{
    [Fact]
    public void Calculate_WithEqualDurations_ReturnsWeightedMedian()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(2), 30.0)
        ];

        double result = TimeWeightedMedian.Calculate(
            observations,
            start.AddHours(3));

        Assert.Equal(20.0, result);
    }

    [Fact]
    public void Calculate_WithUnequalDurations_ReturnsValueWithMedianDuration()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(4), 30.0)
        ];

        double result = TimeWeightedMedian.Calculate(
            observations,
            start.AddHours(5));

        Assert.Equal(20.0, result);
    }

    [Fact]
    public void Calculate_WhenOneValueExceedsHalfDuration_ReturnsThatValue()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddMinutes(1), 20.0)
        ];

        double result = TimeWeightedMedian.Calculate(
            observations,
            start.AddHours(1));

        Assert.Equal(20.0, result);
    }

    [Fact]
    public void Calculate_WithUnsortedTimestamps_ThrowsArgumentException()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(2), 20.0),
            new(start.AddHours(1), 30.0)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeWeightedMedian.Calculate(
                observations,
                start.AddHours(3)));
    }

    [Fact]
    public void Calculate_WithDuplicateTimestamps_ThrowsArgumentException()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 10.0),
            new(start, 20.0)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeWeightedMedian.Calculate(
                observations,
                start.AddHours(1)));
    }

    [Fact]
    public void Calculate_WithEndTimestampNotAfterLastObservation_ThrowsArgumentException()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeWeightedMedian.Calculate(
                observations,
                start.AddHours(1)));
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => TimeWeightedMedian.Calculate(
                Array.Empty<Observation>(),
                DateTimeOffset.UnixEpoch.AddHours(1)));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimeWeightedMedian.Calculate(
                null!,
                DateTimeOffset.UnixEpoch.AddHours(1)));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithNonFiniteValue_ThrowsArgumentException(
        double value)
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, value)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeWeightedMedian.Calculate(
                observations,
                start.AddHours(1)));
    }
}