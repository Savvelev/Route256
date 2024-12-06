using Route256.BLL.DeliveryPrice.Models;

namespace Route256.BLL.DeliveryPrice;

public interface IDeliveryPriceService
{
    public decimal CalculateDeliveryPriceV1(GoodsModel[]? goodsModels);
    public decimal CalculateDeliveryPriceV2(DeliveryModel deliveryModels);
    
    public Cargo[] GetHistoryCargos(int CountItems);
    
    public void DeleteHistoryCargos();
}