namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class FirstDifferenceTests
{
    [Fact]
    public void Calculate_WithIncreasingValues_ReturnsDifferences()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddDays(1), 15.0),
            new(start.AddDays(2), 23.0)
        ];

        var result = FirstDifference.Calculate(observations);

        Assert.Equal(2, result.Count);
        Assert.Equal(5.0, result[0].Value);
        Assert.Equal(8.0, result[1].Value);
    }

    [Fact]
    public void Calculate_WithDecreasingValues_ReturnsNegativeDifferences()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 20.0),
            new(start.AddDays(1), 12.0),
            new(start.AddDays(2), 5.0)
        ];

        var result = FirstDifference.Calculate(observations);

        Assert.Equal(-8.0, result[0].Value);
        Assert.Equal(-7.0, result[1].Value);
    }

    [Fact]
    public void Calculate_WithMixedValues_ReturnsCorrectDifferences()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddDays(1), 15.0),
            new(start.AddDays(2), 8.0)
        ];

        var result = FirstDifference.Calculate(observations);

        Assert.Equal(2, result.Count);
        Assert.Equal(5.0, result[0].Value);
        Assert.Equal(-7.0, result[1].Value);
    }

    [Fact]
    public void Calculate_WithSingleObservation_ReturnsEmptyResult()
    {
        Observation[] observations =
        [
            new(DateTimeOffset.UnixEpoch, 42.0)
        ];

        var result = FirstDifference.Calculate(observations);

        Assert.Empty(result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ReturnsEmptyResult()
    {
        var result = FirstDifference.Calculate(
            Array.Empty<Observation>());

        Assert.Empty(result);
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => FirstDifference.Calculate(null!));
    }

    [Fact]
    public void Calculate_UsesCurrentObservationTimestamp()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;
        DateTimeOffset next = start.AddHours(2);

        Observation[] observations =
        [
            new(start, 10.0),
            new(next, 14.0)
        ];

        var result = FirstDifference.Calculate(observations);

        Assert.Equal(next, result[0].Timestamp);
        Assert.Equal(4.0, result[0].Value);
    }

    [Fact]
    public void Calculate_WithOutOfOrderTimestamps_ThrowsArgumentException()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start.AddDays(1), 10.0),
            new(start, 15.0)
        ];

        Assert.Throws<ArgumentException>(
            () => FirstDifference.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithDuplicateTimestamps_AllowsEqualTimestamps()
    {
        DateTimeOffset timestamp = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(timestamp, 10.0),
            new(timestamp, 15.0)
        ];

        var result = FirstDifference.Calculate(observations);

        Assert.Single(result);
        Assert.Equal(5.0, result[0].Value);
        Assert.Equal(timestamp, result[0].Timestamp);
    }

    [Fact]
    public void Calculate_WithExtremeValues_ThrowsOverflowException()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, -double.MaxValue),
            new(start.AddDays(1), double.MaxValue)
        ];

        Assert.Throws<OverflowException>(
            () => FirstDifference.Calculate(observations));
    }
}