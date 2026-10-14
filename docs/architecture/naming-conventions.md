# Invariant Naming Conventions

## 1. Purpose

This document defines naming conventions for the Invariant codebase, repository, branches, commits, and documentation.

The goal is to maintain a consistent, readable, and predictable codebase that reflects Invariant's design principles: mathematical clarity, source independence, composability, reproducibility, and a minimal core.

## 2. General Principles

- Prefer clear, descriptive names over abbreviations.
- Use established mathematical and statistical terminology where appropriate.
- Name concepts according to their responsibility, not their implementation details.
- Keep names consistent across code, documentation, tests, and public APIs.
- Avoid names tied to a specific data source, vendor, protocol, or storage technology in the core layer.
- Avoid premature abstractions and names that imply capabilities that do not yet exist.
- Prefer singular names for individual domain concepts and plural names for collections.

## 3. C# Naming Conventions

Follow standard .NET naming conventions unless a documented project-specific decision overrides them.

| Element | Convention | Example |
|---|---|---|
| Namespace | PascalCase | `Invariant.Core` |
| Class | PascalCase | `TimeSeries` |
| Struct | PascalCase | `Observation` |
| Interface | PascalCase with `I` prefix | `ITimeSeries` |
| Record | PascalCase | `TimeSeriesPoint` |
| Enum | PascalCase | `ObservationQuality` |
| Enum member | PascalCase | `Good` |
| Method | PascalCase | `CalculateMean()` |
| Property | PascalCase | `Timestamp` |
| Public constant | PascalCase | `DefaultWindowSize` |
| Parameter | camelCase | `windowSize` |
| Local variable | camelCase | `observations` |
| Private field | `_camelCase` | `_timestamp` |
| Generic type parameter | `T` or descriptive `T` prefix | `TValue`, `TModel` |
| Async method | PascalCase with `Async` suffix | `CalculateAsync()` |

### 3.1 Namespaces and Types

Use the `Invariant` root namespace for project-owned code.

Examples:

```csharp
namespace Invariant.Core;

public sealed class Observation<TValue>
{
    public DateTimeOffset Timestamp { get; init; }
    public TValue Value { get; init; } = default!;
}
```

Use namespaces to represent stable conceptual boundaries. Avoid creating namespaces solely to mirror individual files or implementation details.

### 3.2 Interfaces and Implementations

Use an `I` prefix for interfaces. Name implementations by their actual role rather than automatically appending `Impl`.

```csharp
public interface ITimeSeries<TValue>;

public sealed class TimeSeries<TValue>;
```

The examples illustrate naming only; declarations must be completed according to the actual design.

### 3.3 Generic Types

Use descriptive generic parameter names when they improve readability.

```csharp
TimeSeries<TValue>
Observation<TValue>
IComputation<TInput, TResult>
```

Use conventional single-letter names such as `T` only when the meaning is immediately clear.

### 3.4 Mathematical and Statistical Terminology

Prefer established terminology over invented or domain-specific names.

| Prefer | Avoid when equivalent |
|---|---|
| `Mean` | `AverageMagic` |
| `Variance` | `SpreadScore` |
| `StandardDeviation` | `VariationIndex` |
| `Regression` | `TrendEngine` |
| `Observation` | `DataItem` when a precise term is appropriate |
| `Timestamp` | `TimeValue` |

Use terminology precisely. For example, a mean and a median are different statistics and must not be treated as interchangeable names.

### 3.5 Time and Units

Make time semantics and measurement units explicit when ambiguity is possible.

- Use `DateTimeOffset` or another deliberately chosen time representation for timestamps.
- Prefer names such as `ElapsedDuration`, `SampleInterval`, and `WindowDuration` when they accurately describe the concept.
- Include units in names only when they clarify an otherwise ambiguous quantity, such as `TimeoutMilliseconds`.
- Document the time zone, timestamp ordering, interval boundaries, and missing-data behavior where relevant.

Do not encode units inconsistently across otherwise equivalent APIs.

## 4. Project and Package Naming

Use `Invariant` as the project and package prefix.

Potential names include:

- `Invariant.Core`
- `Invariant.Numerics`
- `Invariant.Models`
- `Invariant.Analysis`
- `Invariant.Connectors`

These names are examples of the intended vocabulary, not a commitment to create every package. Package boundaries should follow demonstrated architectural needs.

Use PascalCase for .NET project names and package identifiers unless a distribution platform imposes different requirements.

## 5. File and Directory Naming

- Use PascalCase for C# source files, generally matching the primary type name.
- Use lowercase kebab-case for Markdown documentation filenames.
- Use lowercase kebab-case for documentation directories where practical.
- Use descriptive names for test files, fixtures, and examples.

Examples:

```text
docs/
  naming-conventions.md
  design-principles.md
  architecture.md
  mathematical-vocabulary.md
  reproducibility.md

src/
  Invariant.Core/
    TimeSeries.cs
    Observation.cs

tests/
  Invariant.Core.Tests/
    TimeSeriesTests.cs
```

The directory structure is illustrative and may evolve with the solution.

## 6. Branch Naming

Use the format:

`<type>/<short-description>`

Use lowercase kebab-case for the description.

| Type | Purpose | Example |
|---|---|---|
| `feat/` | New functionality | `feat/time-series-core` |
| `fix/` | Bug fix | `fix/timestamp-validation` |
| `refactor/` | Internal restructuring | `refactor/core-boundaries` |
| `test/` | Test additions or changes | `test/numeric-invariants` |
| `docs/` | Documentation changes | `docs/naming-conventions` |
| `build/` | Build or dependency changes | `build/solution-configuration` |
| `ci/` | Continuous integration changes | `ci/test-workflow` |
| `perf/` | Performance improvements | `perf/window-aggregation` |

Keep branch names concise and specific. Avoid personal names, vague descriptions, and unnecessary ticket identifiers. If the repository adopts an issue-tracking convention, include issue identifiers consistently.

## 7. Commit Messages

Use the Conventional Commits format:

`<type>(<optional-scope>): <description>`

Examples:

```text
feat(core): add time-series abstraction
fix(core): validate observation timestamps
refactor(numerics): clarify aggregation boundaries
test(core): cover empty time series
docs(architecture): document source independence
build: configure nullable reference types
```

Guidelines:

- Use imperative, concise descriptions.
- Describe the change rather than the motivation alone.
- Keep the subject lowercase after the type and scope unless proper nouns require capitalization.
- Use scopes that correspond to meaningful project areas.
- Do not claim mathematical guarantees that the change does not establish.

## 8. Pull Request Titles

Use concise, action-oriented titles that describe the primary change.

Examples:

- `Establish the core time-series abstraction`
- `Define observation timestamp semantics`
- `Add deterministic aggregation primitives`
- `Document reproducibility requirements`
- `Separate source adapters from core computation`

Use a prefix only if the repository adopts a consistent PR-title policy, such as Conventional Commits.

## 9. Tests and Test Data

Name test files after the type or behavior being tested, using the `Tests` suffix.

Examples:

- `TimeSeriesTests.cs`
- `MeanTests.cs`
- `TimestampValidationTests.cs`
- `ReproducibilityTests.cs`

Use descriptive test method names that state the condition and expected behavior. For example:

```csharp
[Fact]
public void CalculateMean_WithKnownValues_ReturnsExpectedResult()
{
    // Arrange, act, and assert.
}
```

Test names should describe observable behavior rather than private implementation details. Mathematical tests should use independently verifiable expected values and explicitly cover relevant edge cases.

## 10. Documentation and Mathematical Vocabulary

Use established mathematical terms consistently throughout source code and documentation.

Distinguish the following concepts:

- **Time series:** Timestamped observations represented under defined semantics.
- **Computation:** An operation that calculates a result from defined inputs.
- **Model:** A representation or estimate of underlying behavior.
- **Analysis:** The interpretation or combination of computations and models to address a question.
- **Result:** The output of a computation or analysis, with relevant context where appropriate.

These definitions describe the intended conceptual vocabulary. Detailed semantics should be documented as the implementation evolves.

Use `numeric` and `categorical` consistently when referring to the initial value categories. Prefer `category` over `categorical` only when referring to an individual category value.

## 11. API Compatibility and Naming Changes

Public names are part of the library's API. Before changing a public type, method, property, or namespace:

- Assess source and binary compatibility.
- Consider downstream consumers.
- Update tests and documentation.
- Record significant breaking changes according to the project's release policy.

Do not introduce aliases or duplicate names merely to preserve every internal naming experiment. Public compatibility decisions should be deliberate.

## 12. Enforcement

Where practical, enforce conventions through:

- .NET analyzers and editor configuration.
- Consistent formatting and build settings.
- Automated tests.
- Code review.
- Documentation review.

Use `.editorconfig` for applicable C# style rules and configure analyzers centrally where appropriate. Avoid relying on manual review for conventions that can be checked automatically.

## 13. Guiding Rule

Names should make the mathematical meaning, responsibility, and boundaries of a concept clear without tying the core to a particular data source or implementation.

**Invariant separates the origin of data from the mathematics performed on it. Its naming should reflect that separation.**