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
    public decimal CalculateDeliveryPriceV1(GoodsModel[]? goodsModels)
    {
        if (goodsModels == null || goodsModels.Length == 0)
        {
            throw new ArgumentException("No goods models were provided");
        }
        
        
        var deliveryPriceByVolume = CalculatePriceByVolumeV1(goodsModels, out var volumeCm);

        var deliveryPriceByWeight = CalculatePriceByWeightV1(goodsModels, out var weightGg);

        var maxPriceAfterCompared = decimal.Max(deliveryPriceByVolume, deliveryPriceByWeight);

        var cargo = new Cargo(volumeCm,  weightGg, maxPriceAfterCompared);
        
        _storageRepository.SaveCargo(new CargoDb()
        {
            Price = cargo.Price,
            Volume = cargo.Volume,
            Weight = cargo.Weight,
            DateAt = DateTime.UtcNow,
        });
        
        return maxPriceAfterCompared;
    }

    public decimal CalculateDeliveryPriceV2(DeliveryModel deliveryModels)
    {
        if (deliveryModels.Goods == null || deliveryModels.Goods.Count == 0)
        {
            throw new ArgumentException("No goods were provided");
        }

        if(deliveryModels.Distance < 1)
        {
            throw new ArgumentException("Distance is less than 1 meter");
        }
        
        
        var deliveryPriceByVolume = CalculatePriceByVolumeV2(deliveryModels, out var volumeCm);

        var deliveryPriceByWeight = CalculatePriceByWeightV2(deliveryModels, out var weightGg);

        var maxPriceAfterCompared = decimal.Max(deliveryPriceByVolume, deliveryPriceByWeight);

        var cargo = new Cargo(volumeCm,  weightGg, maxPriceAfterCompared, deliveryModels.Distance);
        
        _storageRepository.SaveCargo(new CargoDb()
        {
            Price = cargo.Price,
            Volume = cargo.Volume,
            Weight = cargo.Weight,
            DateAt = DateTime.UtcNow,
            Distance = (int)cargo.Distance!,
        });
        
        return maxPriceAfterCompared;
        
    }

    private static decimal CalculatePriceByWeightV1(GoodsModel[] goodsModels, out int weightGg)
    {
        weightGg = goodsModels
            .Sum(x=>x.Weight * x.Weight) / 1000 ?? 0;

        var deliveryPriceByWeight = (decimal)(weightGg * DeliveryCoefficientByWeight);
        return deliveryPriceByWeight;
    }

    private static decimal CalculatePriceByVolumeV1(GoodsModel[] goodsModels, out int volumeCm)
    {
        var volumeМм = goodsModels
            .Select(x => x.Height * x.Lenght * x.Wight)
            .Sum();

        volumeCm = volumeМм / 1000;
        
        var deliveryPriceByVolume = (decimal)(volumeCm * DeliveryCoefficient);
        return deliveryPriceByVolume;
    } 
    
    private static decimal CalculatePriceByWeightV2(DeliveryModel deliveryModel, out int weightGg)
    {
        weightGg = deliveryModel.Goods!
            .Sum(x=>x.Weight * x.Weight) / 1000 ?? 0;

        var deliveryPriceByWeight = (decimal)(weightGg * DeliveryCoefficientByWeight) * deliveryModel.Distance;
        return deliveryPriceByWeight;
    }

    private static decimal CalculatePriceByVolumeV2(DeliveryModel deliveryModel, out int volumeCm)
    {
        var volumeМм = deliveryModel.Goods!
            .Select(x => x.Height * x.Lenght * x.Wight)
            .Sum();

        volumeCm = volumeМм / 1000;
        
        var deliveryPriceByVolume = (decimal)(volumeCm * DeliveryCoefficient) * deliveryModel.Distance;
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

    public void DeleteHistoryCargos()
    {
       _storageRepository.DeleteAllCargos();
    }
}