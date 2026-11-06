using Invariant.Numerics.TimeSeries;

namespace Invariant.Numerics.Windowing;

/// <summary>
/// The result of a computation evaluated over one window.
/// </summary>
public readonly record struct WindowResult<TResult>(Interval Interval, TResult Value);

/// <summary>
/// Evaluates a computation over each window of a sequence of observations.
/// </summary>
public static class WindowEvaluation
{
    /// <summary>
    /// Restricts <paramref name="observations"/> to each window in turn and applies
    /// <paramref name="computation"/> to the restriction. Results are returned in the
    /// order of <paramref name="windows"/>. The observations must be ordered by
    /// ascending timestamp.
    /// </summary>
    public static IReadOnlyList<WindowResult<TResult>> Evaluate<TResult>(
        this ReadOnlyMemory<Observation> observations,
        IReadOnlyList<Interval> windows,
        Func<ReadOnlyMemory<Observation>, TResult> computation)
    {
        ArgumentNullException.ThrowIfNull(windows);
        ArgumentNullException.ThrowIfNull(computation);

        var results = new WindowResult<TResult>[windows.Count];

        for (int index = 0; index < windows.Count; index++)
        {
            Interval window = windows[index];
            ReadOnlyMemory<Observation> restriction = observations.Restrict(window);

            results[index] = new WindowResult<TResult>(window, computation(restriction));
        }

        return results;
    }
}