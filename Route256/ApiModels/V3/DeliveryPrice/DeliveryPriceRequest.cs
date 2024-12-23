namespace Route256.ApiModels.V3.DeliveryPrice;

public record DeliveryPriceRequest
{
    public GoodsRequest[]? Goods { get; init; }
    
    public int Distance { get; init; }
}

public record GoodsRequest
{
    public int Length { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public int Weight { get; init; }
}