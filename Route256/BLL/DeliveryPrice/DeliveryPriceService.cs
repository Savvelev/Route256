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
        
        
        var deliveryPriceByVolume = CalculatePriceByVolume(goodsModels, out var volumeCm);

        var deliveryPriceByWeight = CalculatePriceByWeight(goodsModels, out var weightGg);

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

    private static decimal CalculatePriceByWeight(GoodsModel[] goodsModels, out int weightGg)
    {
        weightGg = goodsModels
            .Sum(x=>x.Weight * x.Weight) / 1000 ?? 0;

        var deliveryPriceByWeight = (decimal)(weightGg * DeliveryCoefficientByWeight);
        return deliveryPriceByWeight;
    }

    private static decimal CalculatePriceByVolume(GoodsModel[] goodsModels, out int volumeCm)
    {
        var volumeМм = goodsModels
            .Select(x => x.Height * x.Lenght * x.Wight)
            .Sum();

        volumeCm = volumeМм / 1000;
        
        var deliveryPriceByVolume = (decimal)(volumeCm * DeliveryCoefficient);
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