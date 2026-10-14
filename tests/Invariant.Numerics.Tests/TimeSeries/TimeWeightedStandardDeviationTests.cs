namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class TimeWeightedStandardDeviationTests
{
    [Fact]
    public void Calculate_WithEqualIntervals_ReturnsStandardDeviation()
    {
        DateTime start = new(2026, 1, 1);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(2), 30.0)
        ];

        double result =
            TimeWeightedStandardDeviation.Calculate(observations);

        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Calculate_WithUnequalIntervals_ReturnsTimeWeightedStandardDeviation()
    {
        DateTime start = new(2026, 1, 1);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(4), 30.0)
        ];

        double result =
            TimeWeightedStandardDeviation.Calculate(observations);

        Assert.Equal(Math.Sqrt(18.75), result);
    }

    [Fact]
    public void Calculate_WithConstantValues_ReturnsZero()
    {
        DateTime start = new(2026, 1, 1);

        Observation[] observations =
        [
            new(start, 42.0),
            new(start.AddHours(1), 42.0),
            new(start.AddHours(3), 42.0)
        ];

        double result =
            TimeWeightedStandardDeviation.Calculate(observations);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimeWeightedStandardDeviation.Calculate(null!));
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => TimeWeightedStandardDeviation.Calculate(
                Array.Empty<Observation>()));
    }

    [Fact]
    public void Calculate_WithSingleObservation_ThrowsArgumentException()
    {
        Observation[] observations =
        [
            new(new DateTime(2026, 1, 1), 10.0)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeWeightedStandardDeviation.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithUnsortedTimestamps_ThrowsArgumentException()
    {
        DateTime start = new(2026, 1, 1);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(2), 20.0),
            new(start.AddHours(1), 30.0)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeWeightedStandardDeviation.Calculate(observations));
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
            new(new DateTime(2026, 1, 1), value),
            new(new DateTime(2026, 1, 1, 1, 0, 0), 20.0)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeWeightedStandardDeviation.Calculate(observations));
    }
}