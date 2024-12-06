namespace Route256.DAL.Entities;

public class CargoDb
{
    public double Volume { get; set; }
    
    public double Weight { get; set; }
    
    public decimal Price { get; set; }
    
    public DateTime DateAt { get; set; }

    public int? Distance { get; set; } 
    
    public int? CountGoods { get; set; }
}
