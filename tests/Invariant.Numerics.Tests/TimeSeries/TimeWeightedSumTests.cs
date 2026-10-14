namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class TimeWeightedSumTests
{
    [Fact]
    public void Calculate_WithEqualIntervals_ReturnsTimeWeightedSum()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(2), 30.0)
        ];

        // 10 × 1 hour + 20 × 1 hour
        Assert.Equal(30.0, TimeWeightedSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithUnequalIntervals_ReturnsTimeWeightedSum()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(4), 30.0)
        ];

        // 10 × 1 hour + 20 × 3 hours
        Assert.Equal(70.0, TimeWeightedSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsTimeWeightedSum()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, -10.0),
            new(start.AddHours(1), -20.0),
            new(start.AddHours(2), 30.0)
        ];

        Assert.Equal(-30.0, TimeWeightedSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithZeroValues_ReturnsZero()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 0.0),
            new(start.AddHours(1), 0.0),
            new(start.AddHours(2), 0.0)
        ];

        Assert.Equal(0.0, TimeWeightedSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithFractionalIntervals_ReturnsTimeWeightedSum()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 12.0),
            new(start.AddMinutes(30), 24.0),
            new(start.AddHours(1), 100.0)
        ];

        // 12 × 0.5 hour + 24 × 0.5 hour
        Assert.Equal(18.0, TimeWeightedSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithUnsortedTimestamps_ThrowsArgumentException()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(2), 20.0),
            new(start.AddHours(1), 30.0)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeWeightedSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithDuplicateTimestamps_ThrowsArgumentException()
    {
        DateTimeOffset timestamp =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(timestamp, 10.0),
            new(timestamp, 20.0)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeWeightedSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithSingleObservation_ThrowsArgumentException()
    {
        Observation[] observations =
        [
            new(
                new DateTimeOffset(
                    2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
                10.0)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeWeightedSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => TimeWeightedSum.Calculate(
                Array.Empty<Observation>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimeWeightedSum.Calculate(null!));
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
            new(start, value),
            new(start.AddHours(1), 20.0)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeWeightedSum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithExtremeValues_ThrowsOverflowException()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, double.MaxValue),
            new(start.AddHours(2), 20.0)
        ];

        Assert.Throws<OverflowException>(
            () => TimeWeightedSum.Calculate(observations));
    }
}