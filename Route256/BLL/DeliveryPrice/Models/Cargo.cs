namespace Route256.BLL.DeliveryPrice.Models;

public class Cargo
{
    public Cargo(decimal volume, decimal price)
    {
        Volume = volume;
        Price = price;
    }

    public decimal Volume { get; set; }
    
    public decimal Price { get; set; }
}