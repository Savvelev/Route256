namespace Route256.Models.V1.DeliveryPrice;

public record DeliveryPriceRequest
{
    public GoodsRequest[]? Goods { get; init; }
}

public record GoodsRequest
{
    public int Length { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
}