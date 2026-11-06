using Invariant.Numerics.TimeSeries;
using Invariant.Numerics.Windowing;
using Xunit;

namespace Invariant.Numerics.Tests.Windowing;

public class WindowEvaluationTests
{
    private const int SecondsPerHour = 3600;
    private const int SecondsPerDay = 86400;

    private static readonly DateTimeOffset Start = new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

    // One observation per second for 24 hours; the value equals the second index: 0, 1, ..., 86399.
    private static ReadOnlyMemory<Observation> CreateDay()
    {
        var observations = new Observation[SecondsPerDay];

        for (int second = 0; second < SecondsPerDay; second++)
        {
            observations[second] = new Observation(Start.AddSeconds(second), second);
        }

        return observations;
    }

    private static IReadOnlyList<Interval> CreateHourlyWindows() =>
        Window.Partition(new Interval(Start, Start.AddHours(24)), TimeSpan.FromHours(1));

    // Each window's result is simply the observations that fall inside it.
    private static IReadOnlyList<WindowResult<ReadOnlyMemory<Observation>>> CreateHourlyPartitions() =>
        CreateDay().Evaluate(CreateHourlyWindows(), restriction => restriction);

    [Fact]
    public void GeneratedDataSet_HasOneAccurateObservationPerSecondForTwentyFourHours()
    {
        ReadOnlySpan<Observation> day = CreateDay().Span;

        Assert.Equal(SecondsPerDay, day.Length);

        for (int second = 0; second < day.Length; second++)
        {
            Assert.Equal(Start.AddSeconds(second), day[second].Timestamp);
            Assert.Equal((double)second, day[second].Value);
        }
    }

    [Fact]
    public void Evaluate_HourlyWindows_PartitionTheDataSetWithoutLossOrDuplication()
    {
        IReadOnlyList<WindowResult<ReadOnlyMemory<Observation>>> partitions = CreateHourlyPartitions();

        Assert.Equal(24, partitions.Count);

        int total = 0;

        foreach (WindowResult<ReadOnlyMemory<Observation>> partition in partitions)
        {
            Assert.Equal(SecondsPerHour, partition.Value.Length);
            total += partition.Value.Length;
        }

        Assert.Equal(SecondsPerDay, total);
    }

    [Fact]
    public void Evaluate_HourlyWindows_EveryObservationInEachPartitionIsAccurate()
    {
        IReadOnlyList<WindowResult<ReadOnlyMemory<Observation>>> partitions = CreateHourlyPartitions();

        for (int hour = 0; hour < 24; hour++)
        {
            WindowResult<ReadOnlyMemory<Observation>> partition = partitions[hour];
            ReadOnlySpan<Observation> observations = partition.Value.Span;

            for (int offset = 0; offset < observations.Length; offset++)
            {
                int expectedSecond = (hour * SecondsPerHour) + offset;

                Assert.Equal(Start.AddSeconds(expectedSecond), observations[offset].Timestamp);
                Assert.Equal((double)expectedSecond, observations[offset].Value);
                Assert.True(partition.Interval.Contains(observations[offset].Timestamp));
            }
        }
    }
}