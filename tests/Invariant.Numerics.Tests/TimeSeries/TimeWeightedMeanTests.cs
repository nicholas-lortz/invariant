namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class TimeWeightedMeanTests
{
    [Fact]
    public void Calculate_WithEqualIntervals_ReturnsTimeWeightedMean()
    {
        DateTime start = new(2026, 1, 1, 0, 0, 0);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(2), 30.0)
        ];

        double result = TimeWeightedMean.Calculate(observations);

        Assert.Equal(15.0, result);
    }

    [Fact]
    public void Calculate_WithUnequalIntervals_ReturnsTimeWeightedMean()
    {
        DateTime start = new(2026, 1, 1, 0, 0, 0);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(4), 30.0)
        ];

        double result = TimeWeightedMean.Calculate(observations);

        Assert.Equal(17.5, result);
    }

    [Fact]
    public void Calculate_WithUnsortedTimestamps_ThrowsArgumentException()
    {
        DateTime start = new(2026, 1, 1, 0, 0, 0);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(2), 20.0),
            new(start.AddHours(1), 30.0)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeWeightedMean.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithDuplicateTimestamps_ThrowsArgumentException()
    {
        DateTime timestamp = new(2026, 1, 1, 0, 0, 0);

        Observation[] observations =
        [
            new(timestamp, 10.0),
            new(timestamp, 20.0)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeWeightedMean.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithSingleObservation_ThrowsArgumentException()
    {
        Observation[] observations =
        [
            new(new DateTime(2026, 1, 1), 10.0)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeWeightedMean.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => TimeWeightedMean.Calculate(
                Array.Empty<Observation>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimeWeightedMean.Calculate(null!));
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
            () => TimeWeightedMean.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithConstantValues_ReturnsConstantValue()
    {
        DateTime start = new(2026, 1, 1, 0, 0, 0);

        Observation[] observations =
        [
            new(start, 42.0),
            new(start.AddMinutes(1), 42.0),
            new(start.AddHours(5), 42.0)
        ];

        double result = TimeWeightedMean.Calculate(observations);

        Assert.Equal(42.0, result);
    }
}