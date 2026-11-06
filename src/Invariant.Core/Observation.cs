namespace Invariant.Core;

/// <summary>
/// A single timestamped observation of a value.
/// Timestamps are compared by instant (UTC), not by offset.
/// </summary>
/// <typeparam name="TValue">The type of the observed value, such as <see cref="double"/> for a numeric series.</typeparam>
public readonly record struct Observation<TValue>(DateTimeOffset Timestamp, TValue Value);