namespace Invariant.Numerics.Tests.TimeSeries;

public sealed class TimeSeriesMeanTests
{
    [Fact]
    public void Calculate_WithSequentialValues_ReturnsArithmeticMean()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddHours(1), 20.0),
            new(start.AddHours(2), 30.0)
        ];

        Assert.Equal(20.0, TimeSeriesMean.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithUnequalIntervals_ReturnsArithmeticMean()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 10.0),
            new(start.AddMinutes(1), 20.0),
            new(start.AddHours(10), 30.0)
        ];

        Assert.Equal(20.0, TimeSeriesMean.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithUnsortedTimestamps_ReturnsArithmeticMean()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start.AddHours(2), 30.0),
            new(start, 10.0),
            new(start.AddHours(1), 20.0)
        ];

        Assert.Equal(20.0, TimeSeriesMean.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsArithmeticMean()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, -10.0),
            new(start.AddHours(1), 0.0),
            new(start.AddHours(2), 10.0)
        ];

        double result = TimeSeriesMean.Calculate(observations);

        Assert.Equal(0.0, result, precision: 10);
    }

    [Fact]
    public void Calculate_WithFractionalValues_ReturnsArithmeticMean()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 1.5),
            new(start.AddMinutes(1), 2.5),
            new(start.AddMinutes(2), 5.0)
        ];

        Assert.Equal(3.0, TimeSeriesMean.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithSingleObservation_ReturnsItsValue()
    {
        Observation[] observations =
        [
            new(
                new DateTimeOffset(
                    2026, 1, 1, 0, 0, 0, TimeSpan.Zero),
                42.0)
        ];

        Assert.Equal(42.0, TimeSeriesMean.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithIdenticalValues_ReturnsThatValue()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, 42.0),
            new(start.AddMinutes(1), 42.0),
            new(start.AddHours(5), 42.0)
        ];

        Assert.Equal(42.0, TimeSeriesMean.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithOppositeExtremeValues_ReturnsZero()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, -double.MaxValue),
            new(start.AddMinutes(1), double.MaxValue)
        ];

        Assert.Equal(0.0, TimeSeriesMean.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithEmptySequence_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(
            () => TimeSeriesMean.Calculate(
                Array.Empty<Observation>()));
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimeSeriesMean.Calculate(null!));
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
            new(start, value)
        ];

        Assert.Throws<ArgumentException>(
            () => TimeSeriesMean.Calculate(observations));
    }

    [Fact]
    public void Calculate_WithLargeFiniteValues_ReturnsFiniteMean()
    {
        DateTimeOffset start =
            new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Observation[] observations =
        [
            new(start, double.MaxValue),
            new(start.AddMinutes(1), double.MaxValue)
        ];

        Assert.Equal(
            double.MaxValue,
            TimeSeriesMean.Calculate(observations));
    }
}