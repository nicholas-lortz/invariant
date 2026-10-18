using System.Text.Json;
using Invariant.Numerics.TimeSeries;

var path = Path.Combine(AppContext.BaseDirectory, "data", "samples.json");
var datasets = JsonSerializer.Deserialize<Dictionary<string, SamplePoint[]>>(
    File.ReadAllText(path),
    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

var series = datasets["known_values"]
    .Select(p => new Observation(p.Timestamp, p.Value))
    .ToArray();

double[] weights = [1, 1, 1, 1, 2, 2, 3, 3];

// Summary statistics
Console.WriteLine($"Sum: {TimeSeriesSum.Calculate(series)}");
Console.WriteLine($"Mean: {TimeSeriesMean.Calculate(series)}");
Console.WriteLine($"Median: {TimeSeriesMedian.Calculate(series)}");
Console.WriteLine($"Mode: {TimeSeriesMode.Calculate(series)}");
Console.WriteLine($"Quantile (0.5): {TimeSeriesQuantile.Calculate(series, 0.5)}");
Console.WriteLine($"Range: {TimeSeriesRange.Calculate(series)}");
Console.WriteLine($"MinMax: {TimeSeriesMinMax.Calculate(series)}");

// Time-weighted statistics
Console.WriteLine($"Time-Weighted Sum: {TimeWeightedSum.Calculate(series)}");
Console.WriteLine($"Time-Weighted Integral: {TimeWeightedIntegral.Calculate(series)}");
Console.WriteLine($"Time-Weighted Minimum: {TimeWeightedMinimum.Calculate(series)}");
Console.WriteLine($"Time-Weighted Maximum: {TimeWeightedMaximum.Calculate(series)}");
Console.WriteLine($"Time-Weighted Variance: {TimeWeightedVariance.Calculate(series)}");
Console.WriteLine($"Time-Weighted Standard Deviation: {TimeWeightedStandardDeviation.Calculate(series)}");

// Transformations (these return a series, so print the first few points)
PrintSeries("First Difference", FirstDifference.Calculate(series));
PrintSeries("Log Return", LogReturn.Calculate(series));
PrintSeries("Percentage Change", PercentageChange.Calculate(series));

static void PrintSeries(string label, IEnumerable<Observation> s) =>
    Console.WriteLine($"{label}: " +
        string.Join(", ", s.Take(3).Select(o => $"{o.Timestamp:HH:mm}={o.Value:G6}")) + " ...");

Console.ReadLine();

record SamplePoint(DateTimeOffset Timestamp, double Value);