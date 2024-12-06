namespace Route256.BLL.DeliveryPrice.Models;

public class DeliveryModel
{
    public IList<GoodsModel>? Goods { get; set; }
    
    public int Distance { get; set; }
}