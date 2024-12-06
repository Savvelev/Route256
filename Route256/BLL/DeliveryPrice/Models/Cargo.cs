namespace Route256.BLL.DeliveryPrice.Models;

public class Cargo
{
    public Cargo(decimal volume, decimal weight, decimal price, int? distance = null)
    {
        Volume = volume;
        Weight = weight;
        Price = price;
        Distance = distance;
    }

    public decimal Volume { get; set; }
    
    public decimal Weight { get; set; }
    
    public decimal Price { get; set; }
    
    public int? Distance { get; set; }
}