namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class TimeWeightedQuantileTests
{
    [Fact]
    public void Calculate_WithMedianProbability_ReturnsWeightedMedian()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(2), 30.0)
        ];

        double result = TimeWeightedQuantile.Calculate(
            observations,
            start.AddHours(3),
            0.5);

        Assert.Equal(20.0, result);
    }

    [Fact]
    public void Calculate_WithUnequalDurations_ReturnsDurationWeightedQuantile()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(4), 30.0)
        ];

        double result = TimeWeightedQuantile.Calculate(
            observations,
            start.AddHours(5),
            0.5);

        Assert.Equal(20.0, result);
    }

    [Theory]
    [InlineData(0.0, 10.0)]
    [InlineData(1.0, 30.0)]
    public void Calculate_WithBoundaryProbability_ReturnsEndpoint(
        double probability,
        double expected)
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(2), 30.0)
        ];

        double result = TimeWeightedQuantile.Calculate(
            observations,
            start.AddHours(3),
            probability);

        Assert.Equal(expected, result);
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
            () => TimeWeightedQuantile.Calculate(
                observations,
                start.AddHours(3),
                0.5));
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
            () => TimeWeightedQuantile.Calculate(
                observations,
                start.AddHours(1),
                0.5));
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => TimeWeightedQuantile.Calculate(
                Array.Empty<Observation>(),
                DateTimeOffset.UnixEpoch.AddHours(1),
                0.5));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimeWeightedQuantile.Calculate(
                null!,
                DateTimeOffset.UnixEpoch.AddHours(1),
                0.5));
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(1.1)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithInvalidProbability_ThrowsArgumentOutOfRangeException(
        double probability)
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 10.0)
        ];

        Assert.Throws<ArgumentOutOfRangeException>(
            () => TimeWeightedQuantile.Calculate(
                observations,
                start.AddHours(1),
                probability));
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
            () => TimeWeightedQuantile.Calculate(
                observations,
                start.AddHours(1),
                0.5));
    }
}