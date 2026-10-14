namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class TimeWeightedMinimumTests
{
    [Fact]
    public void Calculate_WithIncreasingValues_ReturnsFirstValue()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(2), 30.0)
        ];

        Assert.Equal(10.0, TimeWeightedMinimum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithDecreasingValues_ReturnsMinimumWithDuration()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 30.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(2), 10.0)
        ];

        Assert.Equal(20.0, TimeWeightedMinimum.Calculate(observations));
    }

    [Fact]
    public void Calculate_ExcludesFinalObservationWithoutDuration()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(2), -100.0)
        ];

        Assert.Equal(10.0, TimeWeightedMinimum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsMinimum()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, -5.0),
            new(start.AddHours(1), -20.0),
            new(start.AddHours(2), 10.0)
        ];

        Assert.Equal(-20.0, TimeWeightedMinimum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithConstantValues_ReturnsConstantValue()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 42.0),
            new(start.AddHours(1), 42.0),
            new(start.AddHours(5), 42.0)
        ];

        Assert.Equal(42.0, TimeWeightedMinimum.Calculate(observations));
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
            () => TimeWeightedMinimum.Calculate(observations));
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
            () => TimeWeightedMinimum.Calculate(observations));
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
            () => TimeWeightedMinimum.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => TimeWeightedMinimum.Calculate(
                Array.Empty<Observation>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimeWeightedMinimum.Calculate(null!));
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
            () => TimeWeightedMinimum.Calculate(observations));
    }
}