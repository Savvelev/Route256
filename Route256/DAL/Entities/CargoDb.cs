namespace Route256.DAL.Entities;

public class CargoDb
{
    public decimal Volume { get; set; }
    
    public decimal Weight { get; set; }
    
    public decimal Price { get; set; }
    
    public DateTime DateAt { get; set; }

    public int Distance { get; set; } 
}
