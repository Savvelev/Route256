using Route256.BLL.DeliveryPrice.Models;

namespace Route256.BLL.DeliveryPrice;

public interface IDeliveryPriceService
{
    public decimal CalculateDeliveryPrice(GoodsModel [] goodsModels);
    
    public Cargo[] GetHistoryCargos(int CountItems);
}