namespace Route256.BLL.DeliveryPrice.Models;

public class Cargo
{
    public Cargo(decimal volume, decimal weight, decimal price)
    {
        Volume = volume;
        Weight = weight;
        Price = price;
    }

    public decimal Volume { get; set; }
    
    public decimal Weight { get; set; }
    
    public decimal Price { get; set; }
}