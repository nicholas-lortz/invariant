using System.Runtime.InteropServices;
using Invariant.Numerics.TimeSeries;
using Invariant.Numerics.Windowing;
using Xunit;

namespace Invariant.Numerics.Tests.Windowing;

/// <summary>
/// Runs the real <see cref="TimeSeriesMean"/> over hourly partitions of a full day of
/// 1-second data whose value equals the second index: 0, 1, 2, ..., 86399.
/// For hour h the mean is 3600h + 1799.5; for the whole day it is 43199.5.
/// </summary>
public class PartitionStatisticsTests
{
    private const int SecondsPerHour = 3600;
    private const int SecondsPerDay = 86400;

    private static readonly DateTimeOffset Start = new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

    // The one place that knows how to construct an Observation.
    // If Observation is constructed differently, change only this method.
    private static Observation CreateObservation(DateTimeOffset timestamp, double value) =>
        new(timestamp, value);

    private static ReadOnlyMemory<Observation> CreateDay()
    {
        var observations = new Observation[SecondsPerDay];

        for (int second = 0; second < SecondsPerDay; second++)
        {
            observations[second] = CreateObservation(Start.AddSeconds(second), second);
        }

        return observations;
    }

    private static IReadOnlyList<Interval> CreateHourlyPartitions() =>
        Window.Partition(new Interval(Start, Start.AddHours(24)), TimeSpan.FromHours(1));

    // Adapter: a partition is a ReadOnlyMemory<Observation>; TimeSeriesMean takes IEnumerable<Observation>.
    private static double Mean(ReadOnlyMemory<Observation> restriction) =>
        TimeSeriesMean.Calculate(MemoryMarshal.ToEnumerable(restriction));

    [Fact]
    public void Evaluate_TimeSeriesMeanOverHourlyPartitions_ReturnsClosedFormValues()
    {
        IReadOnlyList<WindowResult<double>> results = CreateDay().Evaluate(CreateHourlyPartitions(), Mean);

        Assert.Equal(24, results.Count);

        for (int hour = 0; hour < 24; hour++)
        {
            Assert.Equal((SecondsPerHour * hour) + 1799.5, results[hour].Value, precision: 6);
        }
    }

    [Fact]
    public void TimeSeriesMean_OverTheWholeDay_ReturnsClosedFormValue()
    {
        double mean = Mean(CreateDay());

        Assert.Equal(43199.5, mean, precision: 6);
    }

    [Fact]
    public void Evaluate_MeanOfHourlyMeans_EqualsMeanOfTheWholeDay()
    {
        ReadOnlyMemory<Observation> day = CreateDay();

        IReadOnlyList<WindowResult<double>> hourlyMeans = day.Evaluate(CreateHourlyPartitions(), Mean);

        // Valid because every hour holds the same number of observations.
        Assert.Equal(Mean(day), hourlyMeans.Average(result => result.Value), precision: 6);
    }
}