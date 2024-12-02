using Route256.BLL.DeliveryPrice.Models;
using Route256.DAL.Repositories;

namespace Route256.BLL.DeliveryPrice;

public class DeliveryPriceService : IDeliveryPriceService
{
    private readonly IStorageRepository _storageRepository;

    public DeliveryPriceService(IStorageRepository storageRepository)
    {
        _storageRepository = storageRepository;
    }
    public decimal CalculateDeliveryPrice(GoodsModel[] goodsModels)
    {
        throw new NotImplementedException();
    }

    public Cargo[] GetHistoryCargos(int CountItems)
    {
        return _storageRepository.GetCargos(CountItems);
    }
}