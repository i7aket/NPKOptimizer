using NPKOptimizer.Common;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace NPKOptimizer.Domain.Fertilizers.ValueObjects;

public record FertilizerMagnesium : ElementFieldBase
{
    public double MgNonChelated { get; }
    public double MgEdta { get; }

    public FertilizerMagnesium(double mgNonChelated = 0, double mgEdta = 0) : base(mgNonChelated + mgEdta)
    {
        ThrowIf.LowerThan(mgNonChelated,0);
        ThrowIf.LowerThan(mgEdta,0);

        MgNonChelated = mgNonChelated;
        MgEdta = mgEdta;
    }
}