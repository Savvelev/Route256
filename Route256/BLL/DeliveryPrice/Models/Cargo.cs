namespace Route256.BLL.DeliveryPrice.Models;

public class Cargo
{
    public Cargo(double volume, double weight, decimal price, int? distance = null)
    {
        Volume = volume;
        Weight = weight;
        Price = price;
        Distance = distance;
    }

    public double Volume { get; set; }
    
    public double Weight { get; set; }
    
    public decimal Price { get; set; }
    
    public int? Distance { get; set; }
}