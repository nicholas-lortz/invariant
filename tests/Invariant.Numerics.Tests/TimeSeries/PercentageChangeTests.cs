namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class PercentageChangeTests
{
    [Fact]
    public void Calculate_WithIncreasingValues_ReturnsPositiveChanges()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 100.0),
            new(start.AddDays(1), 110.0),
            new(start.AddDays(2), 121.0)
        ];

        var result = PercentageChange.Calculate(observations);

        Assert.Equal(2, result.Count);
        Assert.Equal(0.1, result[0].Value, 12);
        Assert.Equal(0.1, result[1].Value, 12);
    }

    [Fact]
    public void Calculate_WithDecreasingValues_ReturnsNegativeChange()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 100.0),
            new(start.AddDays(1), 80.0)
        ];

        var result = PercentageChange.Calculate(observations);

        Assert.Single(result);
        Assert.Equal(-0.2, result[0].Value, 12);
    }

    [Fact]
    public void Calculate_WithUnchangedValues_ReturnsZeroChange()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 42.0),
            new(start.AddDays(1), 42.0)
        ];

        var result = PercentageChange.Calculate(observations);

        Assert.Single(result);
        Assert.Equal(0.0, result[0].Value);
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsCorrectChange()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, -100.0),
            new(start.AddDays(1), -110.0)
        ];

        var result = PercentageChange.Calculate(observations);

        Assert.Single(result);
        Assert.Equal(0.1, result[0].Value, 12);
    }

    [Fact]
    public void Calculate_WithSingleObservation_ReturnsEmptyResult()
    {
        Observation[] observations =
        [
            new(DateTimeOffset.UnixEpoch, 42.0)
        ];

        var result = PercentageChange.Calculate(observations);

        Assert.Empty(result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ReturnsEmptyResult()
    {
        var result = PercentageChange.Calculate(
            Array.Empty<Observation>());

        Assert.Empty(result);
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => PercentageChange.Calculate(null!));
    }

    [Fact]
    public void Calculate_WithZeroPreviousValue_ThrowsArgumentException()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 0.0),
            new(start.AddDays(1), 10.0)
        ];

        Assert.Throws<ArgumentException>(
            () => PercentageChange.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithOutOfOrderTimestamps_ThrowsArgumentException()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start.AddDays(1), 100.0),
            new(start, 110.0)
        ];

        Assert.Throws<ArgumentException>(
            () => PercentageChange.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithDuplicateTimestamps_AllowsEqualTimestamps()
    {
        DateTimeOffset timestamp = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(timestamp, 100.0),
            new(timestamp, 110.0)
        ];

        var result = PercentageChange.Calculate(observations);

        Assert.Single(result);
        Assert.Equal(0.1, result[0].Value, 12);
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
            () => PercentageChange.Calculate(observations));
    }
}