using Route256.BLL.DeliveryPrice.Models;
using Route256.DAL.Entities;
using Route256.DAL.Repositories;

namespace Route256.BLL.DeliveryPrice;

public class DeliveryPriceService : IDeliveryPriceService
{

    private const double DeliveryCoefficient = 3.27;
    private const double DeliveryCoefficientByWeight = 1.34;
    
    private readonly IStorageRepository _storageRepository;

    public DeliveryPriceService(IStorageRepository storageRepository)
    {
        _storageRepository = storageRepository;
    }
    public decimal CalculateDeliveryPrice(GoodsModel[]? goodsModels)
    {
        if (goodsModels == null || goodsModels.Length == 0)
        {
            throw new ArgumentException("No goods models were provided");
        }
        
        const int distanceKm = 1;

        var volumeМм = goodsModels
            .Select(x => x.Height * x.Lenght * x.Wight)
            .Sum() * distanceKm;

        var volumeCm = volumeМм / 1000;
        
        var deliveryPriceByVolume = (decimal)(volumeCm * DeliveryCoefficient);
        
        var weightGg = goodsModels
            .Sum(x=>x.Weight * x.Weight) / 1000 ?? 0;
        
        var deliveryPriceByWeight = (decimal)(weightGg * DeliveryCoefficientByWeight);
        
        var maxPriceAfterCompared = decimal.Max(deliveryPriceByVolume, deliveryPriceByWeight);

        var cargo = new Cargo(volumeCm,  weightGg, maxPriceAfterCompared);
        
        _storageRepository.SaveCargo(new CargoDb()
        {
            Price = cargo.Price,
            Volume = cargo.Volume,
            Weight = cargo.Weight,
            DateAt = DateTime.UtcNow,
        });
        
        return deliveryPriceByVolume;
    }

    public Cargo[] GetHistoryCargos(int countItems)
    {
        var dbCargos =  _storageRepository
            .GetCargos(countItems)
            .ToList();
        
        var orderedCargoEnumerable = dbCargos
            .OrderByDescending(x => x.DateAt);
        
        return orderedCargoEnumerable
            .Select(c=> new Cargo(c.Volume, c.Weight, c.Price))
            .ToArray();
    }
}