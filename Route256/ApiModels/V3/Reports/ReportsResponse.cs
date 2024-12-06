namespace Route256.ApiModels.V3.Reports;

public record ReportsResponse
{
    public int MaxWeight { get; init; }
    public int MaxVolume { get; init; }
    public int MaxDistanceForHeaviestGood { get; init; }
    public int MaxDistanceForLargestGood { get; init; }
    public decimal WavgPrice { get; init; }
}