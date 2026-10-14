namespace Invariant.Numerics.Tests.Statistics;

public sealed class ProductTests
{
    [Fact]
    public void Calculate_WithPositiveValues_ReturnsProduct()
    {
        double[] values = [2.0, 3.0, 4.0];

        double result = Product.Calculate(values);

        Assert.Equal(24.0, result);
    }

    [Fact]
    public void Calculate_WithNegativeValues_ReturnsProduct()
    {
        double[] values = [-2.0, 3.0];

        double result = Product.Calculate(values);

        Assert.Equal(-6.0, result);
    }

    [Fact]
    public void Calculate_WithEvenNumberOfNegativeValues_ReturnsPositiveProduct()
    {
        double[] values = [-2.0, -3.0];

        double result = Product.Calculate(values);

        Assert.Equal(6.0, result);
    }

    [Fact]
    public void Calculate_WithZero_ReturnsZero()
    {
        double[] values = [2.0, 0.0, 5.0];

        double result = Product.Calculate(values);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void Calculate_WithEmptySequence_ReturnsOne()
    {
        double result = Product.Calculate(Array.Empty<double>());

        Assert.Equal(1.0, result);
    }

    [Fact]
    public void Calculate_WithSingleValue_ReturnsThatValue()
    {
        double[] values = [42.0];

        double result = Product.Calculate(values);

        Assert.Equal(42.0, result);
    }

    [Fact]
    public void Calculate_WithNull_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => Product.Calculate(null!));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void Calculate_WithNonFiniteValue_ThrowsArgumentException(
        double value)
    {
        double[] values = [2.0, value, 3.0];

        Assert.Throws<ArgumentException>(
            () => Product.Calculate(values));
    }

    [Fact]
    public void Calculate_WhenProductOverflows_ThrowsOverflowException()
    {
        double[] values = [double.MaxValue, 2.0];

        Assert.Throws<OverflowException>(
            () => Product.Calculate(values));
    }
}