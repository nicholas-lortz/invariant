namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class LogReturnTests
{
    [Fact]
    public void Calculate_WithIncreasingValues_ReturnsPositiveLogReturn()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 100.0),
            new(start.AddDays(1), 110.0)
        ];

        var result = LogReturn.Calculate(observations);

        Assert.Single(result);
        Assert.Equal(Math.Log(1.1), result[0].Value, 12);
    }

    [Fact]
    public void Calculate_WithDecreasingValues_ReturnsNegativeLogReturn()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 100.0),
            new(start.AddDays(1), 80.0)
        ];

        var result = LogReturn.Calculate(observations);

        Assert.Single(result);
        Assert.Equal(Math.Log(0.8), result[0].Value, 12);
    }

    [Fact]
    public void Calculate_WithUnchangedValues_ReturnsZero()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, 42.0),
            new(start.AddDays(1), 42.0)
        ];

        var result = LogReturn.Calculate(observations);

        Assert.Single(result);
        Assert.Equal(0.0, result[0].Value);
    }

    [Fact]
    public void Calculate_WithSingleObservation_ReturnsEmptyResult()
    {
        Observation[] observations =
        [
            new(DateTimeOffset.UnixEpoch, 42.0)
        ];

        var result = LogReturn.Calculate(observations);

        Assert.Empty(result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ReturnsEmptyResult()
    {
        var result = LogReturn.Calculate(
            Array.Empty<Observation>());

        Assert.Empty(result);
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => LogReturn.Calculate(null!));
    }

    [Theory]
    [InlineData(0.0, 10.0)]
    [InlineData(10.0, 0.0)]
    [InlineData(-10.0, 20.0)]
    [InlineData(20.0, -10.0)]
    public void Calculate_WithNonPositiveValue_ThrowsArgumentException(
        double previous,
        double current)
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, previous),
            new(start.AddDays(1), current)
        ];

        Assert.Throws<ArgumentException>(
            () => LogReturn.Calculate(observations));
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
            () => LogReturn.Calculate(observations));
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

        var result = LogReturn.Calculate(observations);

        Assert.Single(result);
        Assert.Equal(Math.Log(1.1), result[0].Value, 12);
        Assert.Equal(timestamp, result[0].Timestamp);
    }

    [Fact]
    public void Calculate_WithExtremePositiveValues_ReturnsFiniteLogReturn()
    {
        DateTimeOffset start = DateTimeOffset.UnixEpoch;

        Observation[] observations =
        [
            new(start, double.Epsilon),
            new(start.AddDays(1), double.MaxValue)
        ];

        var result = LogReturn.Calculate(observations);

        Assert.Single(result);
        Assert.True(double.IsFinite(result[0].Value));
        Assert.True(result[0].Value > 0.0);
    }
}