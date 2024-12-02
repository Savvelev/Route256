using Route256.BLL.DeliveryPrice.Models;
using Route256.DAL.Entities;
using Route256.DAL.Repositories;

namespace Route256.BLL.DeliveryPrice;

public class DeliveryPriceService : IDeliveryPriceService
{

    private const double DeliveryCoefficient = 3.27;
    
    private readonly IStorageRepository _storageRepository;

    public DeliveryPriceService(IStorageRepository storageRepository)
    {
        _storageRepository = storageRepository;
    }
    public decimal CalculateDeliveryPrice(GoodsModel[]? goodsModels)
    {
        if (goodsModels == null || goodsModels.Length == 0)
        {
            return 0;
        }
        
        const int distanceKm = 1;

        var volumeМм = goodsModels
            .Select(x => x.Height * x.Lenght * x.Wight)
            .Sum() * distanceKm;

        var volumeCm = volumeМм / 1000;
        
        var deliveryPrice = (decimal)(volumeCm * DeliveryCoefficient);

        var cargo = new Cargo(volumeCm, deliveryPrice);
        
        _storageRepository.SaveCargo(new CargoDb()
        {
            Price = cargo.Price,
            Volume = volumeCm
        });
        
        return deliveryPrice;
    }

    public Cargo[] GetHistoryCargos(int countItems)
    {
        var dbCargos =  _storageRepository.GetCargos(countItems);
        
        return dbCargos.Select(c=> new Cargo(c.Volume, c.Price)).ToArray();
    }
}