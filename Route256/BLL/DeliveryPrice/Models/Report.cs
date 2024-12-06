namespace Route256.BLL.DeliveryPrice.Models;

public class Report
{
    public int MaxWeight { get; init; }
    public int MaxVolume { get; init; }
    public int MaxDistanceForHeaviestGood { get; init; }
    public int MaxDistanceForLargestGood { get; init; }
    public decimal WavgPrice { get; init; }
}