# invariant

**Open, reproducible mathematical computation for time-series data.**

Invariant is an open-source computation layer for timestamped data.

It is designed to be **source-agnostic**: data can come from an industrial historian, OPC UA, MQTT, SQL, a time-series database, cloud storage, a file, or any other source capable of producing timestamped observations.

Invariant provides a common representation of that data and a trusted foundation for statistical and mathematical computation.

## The idea

Data sources and storage systems should not need to define the mathematics performed on the data.

```text
┌─────────────────────────────────────────────────────────┐
│                     Data Sources                        │
│                                                         │
│   OPC UA   Historian   SQL   MQTT   InfluxDB   Files    │
└───────────────────────────┬─────────────────────────────┘
                            │
                            ▼
                 ┌────────────────────┐
                 │     Invariant      │
                 │                    │
                 │    Time Series     │
                 │         ↓          │
                 │    Computation     │
                 │         ↓          │
                 │       Models       │
                 │         ↓          │
                 │      Analysis      │
                 └─────────┬──────────┘
                           │
                           ▼
                        Results
```
 
**You own the data. Invariant provides the mathematics.**

## Why Invariant?

Industrial and other regulated environments increasingly depend on analytics derived from time-series data.

Yet the mathematical layer is often tightly coupled to:

- a particular historian
- a database
- an analytics platform
- a cloud provider
- a proprietary application
- a particular data pipeline

Invariant aims to separate these concerns.

The same mathematical computation should produce the same result regardless of where the observations originated.

As an open-source project, the implementation can be inspected, tested, versioned, and independently validated.

This is especially valuable where analytical results need to be **understood, reproduced, and trusted**.

## Core concepts

Invariant is built around a simple conceptual pipeline:

```text
Data
  ↓
Computation
  ↓
Model
  ↓
Analysis
  ↓
Result
```

### Time Series

The fundamental data structure is a sequence of timestamped observations.

| Timestamp | Value |
|---|---:|
| 2026-01-01 10:00 | 21.4 |
| 2026-01-01 10:01 | 21.5 |
| 2026-01-01 10:02 | 21.5 |
| ... | ... |

Invariant is intentionally agnostic about where those observations came from.

### Types

The initial value model is deliberately simple:

```text
TimeSeries
├── Numeric
└── Category
```

Numeric series support mathematical and statistical computation.

Category series support operations concerned with frequency, duration, transitions, and sequences.

Additional metadata, such as source quality, can be associated with observations without becoming part of the underlying value type.

### Computations

Computations are mathematical operations performed over observations.

The vocabulary is intended to remain grounded in established mathematical and statistical terminology.

Examples include:

- Descriptive statistics
- Aggregation
- Transformation
- Windowing
- Dependence
- Distribution
- Trend
- Periodicity
- Deviation
- Change
- Prediction

The goal is not to create a collection of domain-specific analytics terms.

Where an established mathematical term exists, Invariant should use it.

### Models

Models describe or estimate underlying behavior using observations and derived computations.

Examples may include:

- Regression
- Forecasting
- Exponential smoothing
- Time-series models
- Change-point models

Models build upon the computation layer rather than replacing it.

### Analysis

Analysis combines computations and models to answer higher-level questions.

Examples might include:

- Trend analysis
- Anomaly analysis
- Forecasting
- Change detection
- Seasonality analysis

The distinction is intentional:

> **Computations calculate. Models represent. Analysis interprets.**

## Source agnostic by design

Invariant does not require a particular storage or acquisition technology.

Connectors can adapt data from systems such as:

- OPC UA
- MQTT / Sparkplug
- Industrial historians
- SQL databases
- Time-series databases
- Cloud storage
- CSV / Parquet
- REST APIs
- Proprietary systems

The connector boundary ends at the time-series abstraction.

```text
Source
  ↓
Connector
  ↓
TimeSeries<T>
  ↓
Invariant
```

The mathematical layer should not need to know whether a value came from OPC UA or a CSV file.

## Designed for trust

Invariant is intended for environments where analytical results need to be more than plausible.

They need to be:

- **Inspectable** — the implementation can be examined.
- **Reproducible** — the same inputs and computation can reproduce a result.
- **Deterministic** — behavior should be well-defined.
- **Versioned** — computations can be tied to specific library versions.
- **Testable** — mathematical behavior can be verified independently.
- **Traceable** — results can eventually carry information about their inputs and computation.

A long-term goal is to make it possible to answer:

> **How exactly was this result calculated?**

That means provenance and reproducibility are architectural concerns, not merely documentation features.

## .NET

Invariant is being developed with **.NET and C#** as the primary platform.

The .NET ecosystem provides a strong foundation for deployment across:

- Windows
- Linux
- Containers
- Cloud environments
- On-premises systems
- Enterprise applications
- Industrial applications

The core should remain independent of specific industrial protocols and infrastructure.

## Architecture

The intended dependency direction is:

```text
Applications
     ↑
  Analysis
     ↑
   Models
     ↑
Computations
     ↑
    Core
     ↑
  Adapters
```

Potential packages may eventually include:

```text
Invariant.Core
Invariant.Numerics
Invariant.Models
Invariant.Analysis
Invariant.Connectors
```

The exact package structure is intentionally not finalized.

The core should remain small and stable.

## What Invariant is not

Invariant is not intended to be:

- A historian
- A time-series database
- An industrial automation platform
- An OPC UA server
- A dashboarding platform
- A data lake
- A proprietary analytics service

Those systems can provide the data.

Invariant provides the computational foundation that operates on it.

## Status

Invariant is currently in the **design and exploration phase**.

The initial work is focused on defining:

1. The fundamental data model
2. The mathematical vocabulary
3. The computation model
4. The distinction between computations, models, and analysis
5. Reproducibility and provenance
6. The .NET architecture
7. Source/connector boundaries

The goal is to establish a coherent foundation before building a large collection of algorithms.

## Design principles

### Source independence

Mathematics should not depend on the system that produced the data.

### Mathematical vocabulary

Prefer established mathematical and statistical terminology over domain-specific terminology.

### Composability

Simple computations should be capable of forming more sophisticated analyses.

### Transparency

Important computations should be understandable and inspectable.

### Reproducibility

Results should be reproducible from known inputs, parameters, and computation versions.

### Minimal core

Keep the foundational abstraction small. Put specialized capabilities above it.

### Domain neutrality

Industrial automation is an important target domain, but the underlying mathematics should remain useful outside it.

## Long-term vision

Invariant aims to become a trusted open-source mathematical foundation for applications that work with time-series data.

The vision is simple:

> **Any data source.  
> One time-series abstraction.  
> Open mathematics.  
> Reproducible results.**
