using System.Text.Json;
using Invariant.Numerics.Statistics;

var path = Path.Combine(AppContext.BaseDirectory, "data", "samples.json");
var datasets = JsonSerializer.Deserialize<Dictionary<string, double[]>>(File.ReadAllText(path))!;

foreach (var (name, values) in datasets)
    Console.WriteLine($"{name}: {values.Length} values");

Console.WriteLine($"Count: {Count.Calculate(datasets["known_values"])}");
Console.WriteLine($"Distinct Count: {DistinctCount.Calculate(datasets["known_values"])}");  
Console.WriteLine($"Geometric Mean: {GeometricMean.Calculate(datasets["known_values"])}");
Console.WriteLine($"Harmonic Mean: {HarmonicMean.Calculate(datasets["known_values"])}");
Console.WriteLine($"Maximum: {Maximum.Calculate(datasets["known_values"])}");
Console.WriteLine($"Mean: {Mean.Calculate(datasets["known_values"])}");
Console.WriteLine($"Mean Absolute Deviation: {MeanAbsoluteDeviation.Calculate(datasets["known_values"])}");
Console.WriteLine($"Median: {Median.Calculate(datasets["known_values"])}");
Console.WriteLine($"Median Absolute Deviation: {MedianAbsoluteDeviation.Calculate(datasets["known_values"])}");
Console.WriteLine($"Minimum: {Minimum.Calculate(datasets["known_values"])}");
Console.WriteLine($"Mode: {Mode.Calculate(datasets["known_values"])}");
Console.WriteLine($"Product: {Product.Calculate(datasets["known_values"])}");
Console.WriteLine($"Range: {Invariant.Numerics.Statistics.Range.Calculate(datasets["known_values"])}");
Console.WriteLine($"Root Mean Square: {RootMeanSquare.Calculate(datasets["known_values"])}");
Console.WriteLine($"Standard Deviation: {StandardDeviation.CalculatePopulation(datasets["known_values"])}");
Console.WriteLine($"Sum: {Sum.Calculate(datasets["known_values"])}");
Console.WriteLine($"Variance: {Variance.CalculatePopulation(datasets["known_values"])}");

Console.ReadLine();