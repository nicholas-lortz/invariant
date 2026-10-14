namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class TimeSeriesQuantileTests
{
    [Fact]
    public void Calculate_WithMedianProbability_ReturnsMedian()
    {
        Observation[] observations =
        [
            new(DateTimeOffset.UnixEpoch, 1.0),
            new(DateTimeOffset.UnixEpoch.AddHours(1), 2.0),
            new(DateTimeOffset.UnixEpoch.AddHours(2), 3.0),
            new(DateTimeOffset.UnixEpoch.AddHours(3), 4.0),
            new(DateTimeOffset.UnixEpoch.AddHours(4), 5.0)
        ];

        double result = TimeSeriesQuantile.Calculate(observations, 0.5);

        Assert.Equal(3.0, result);
    }

    [Fact]
    public void Calculate_WithFirstQuartileProbability_ReturnsFirstQuartile()
    {
        Observation[] observations =
        [
            new(DateTimeOffset.UnixEpoch, 1.0),
            new(DateTimeOffset.UnixEpoch.AddHours(1), 2.0),
            new(DateTimeOffset.UnixEpoch.AddHours(2), 3.0),
            new(DateTimeOffset.UnixEpoch.AddHours(3), 4.0),
            new(DateTimeOffset.UnixEpoch.AddHours(4), 5.0)
        ];

        double result = TimeSeriesQuantile.Calculate(observations, 0.25);

        Assert.Equal(2.0, result);
    }

    [Fact]
    public void Calculate_WithThirdQuartileProbability_ReturnsThirdQuartile()
    {
        Observation[] observations =
        [
            new(DateTimeOffset.UnixEpoch, 1.0),
            new(DateTimeOffset.UnixEpoch.AddHours(1), 2.0),
            new(DateTimeOffset.UnixEpoch.AddHours(2), 3.0),
            new(DateTimeOffset.UnixEpoch.AddHours(3), 4.0),
            new(DateTimeOffset.UnixEpoch.AddHours(4), 5.0)
        ];

        double result = TimeSeriesQuantile.Calculate(observations, 0.75);

        Assert.Equal(4.0, result);
    }

    [Fact]
    public void Calculate_WithUnsortedValues_ReturnsQuantile()
    {
        Observation[] observations =
        [
            new(DateTimeOffset.UnixEpoch, 5.0),
            new(DateTimeOffset.UnixEpoch.AddHours(1), 1.0),
            new(DateTimeOffset.UnixEpoch.AddHours(2), 4.0),
            new(DateTimeOffset.UnixEpoch.AddHours(3), 2.0),
            new(DateTimeOffset.UnixEpoch.AddHours(4), 3.0)
        ];

        double result = TimeSeriesQuantile.Calculate(observations, 0.5);

        Assert.Equal(3.0, result);
    }

    [Fact]
    public void Calculate_WithInterpolation_ReturnsInterpolatedValue()
    {
        Observation[] observations =
        [
            new(DateTimeOffset.UnixEpoch, 10.0),
            new(DateTimeOffset.UnixEpoch.AddHours(1), 20.0),
            new(DateTimeOffset.UnixEpoch.AddHours(2), 30.0),
            new(DateTimeOffset.UnixEpoch.AddHours(3), 40.0)
        ];

        double result = TimeSeriesQuantile.Calculate(observations, 0.25);

        Assert.Equal(17.5, result);
    }

    [Theory]
    [InlineData(0.0, 1.0)]
    [InlineData(1.0, 5.0)]
    public void Calculate_WithBoundaryProbability_ReturnsEndpoint(
        double probability,
        double expected)
    {
        Observation[] observations =
        [
            new(DateTimeOffset.UnixEpoch, 1.0),
            new(DateTimeOffset.UnixEpoch.AddHours(1), 3.0),
            new(DateTimeOffset.UnixEpoch.AddHours(2), 5.0)
        ];

        double result =
            TimeSeriesQuantile.Calculate(observations, probability);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Calculate_WithSingleObservation_ReturnsItsValue()
    {
        Observation[] observations =
        [
            new(DateTimeOffset.UnixEpoch, 42.0)
        ];

        double result = TimeSeriesQuantile.Calculate(observations, 0.75);

        Assert.Equal(42.0, result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => TimeSeriesQuantile.Calculate(
                Array.Empty<Observation>(), 0.5));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimeSeriesQuantile.Calculate(null!, 0.5));
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
        Observation[] observations =
        [
            new(DateTimeOffset.UnixEpoch, 1.0)
        ];

        Assert.Throws<ArgumentOutOfRangeException>(
            () => TimeSeriesQuantile.Calculate(observations, probability));
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
            new(DateTimeOffset.UnixEpoch, value)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeSeriesQuantile.Calculate(observations, 0.5));
    }
}