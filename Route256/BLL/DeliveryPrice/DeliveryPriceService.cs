using Route256.BLL.DeliveryPrice.Models;
using Route256.DAL.Entities;
using Route256.DAL.Repositories;

namespace Route256.BLL.DeliveryPrice;

public class DeliveryPriceService : IDeliveryPriceService
{
    // Коэффициенты для старой версии V1
    private const double DeliveryCoefficientSm2 = 3.27; // для см²
    private const double DeliveryCoefficientByWeight = 1.34; // для г
    
    // Коэффициенты для новой версии V2 (метрическая система)
    private const double DeliveryCoefficientM2 = 0.0327; // для м²
    private const double DeliveryCoefficientByWeightKg = 1.34; // для кг

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
        
        var deliveryPriceByVolume = CalculatePriceByVolumeV1(goodsModels, out var volumeCm3);
        var deliveryPriceByWeight = CalculatePriceByWeightV1(goodsModels, out var weightGg);

        var maxPriceAfterCompared = decimal.Max(deliveryPriceByVolume, deliveryPriceByWeight);

        var cargo = new Cargo(volumeCm3, weightGg, maxPriceAfterCompared);
        
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

        if (deliveryModels.Distance < 1)
        {
            throw new ArgumentException("Distance is less than 1 meter");
        }
        
        var deliveryPriceByVolume = CalculatePriceByVolumeV2(deliveryModels, out var volumeM3);
        var deliveryPriceByWeight = CalculatePriceByWeightV2(deliveryModels, out var weightKg);

        var maxPriceAfterCompared = decimal.Max(deliveryPriceByVolume, deliveryPriceByWeight);

        var cargo = new Cargo(volumeM3, weightKg, maxPriceAfterCompared, deliveryModels.Distance);
        
        _storageRepository.SaveCargo(new CargoDb()
        {
            Price = cargo.Price,
            Volume = cargo.Volume,
            Weight = cargo.Weight,
            DateAt = DateTime.UtcNow,
            Distance = cargo.Distance,
        });
        
        return maxPriceAfterCompared;
    }

    // Методы для V1
    private static decimal CalculatePriceByWeightV1(GoodsModel[] goodsModels, out int weightGg)
    {
        weightGg = goodsModels
            .Sum(x => x.Weight) ?? 0;

        var deliveryPriceByWeight = (decimal)(weightGg * DeliveryCoefficientByWeight);
        return deliveryPriceByWeight;
    }

    private static decimal CalculatePriceByVolumeV1(GoodsModel[] goodsModels, out double volumeCm3)
    {
        var volumeMm3 = goodsModels
            .Select(x => x.Height * x.Lenght * x.Wight)
            .Sum();

        volumeCm3 = volumeMm3 / 1000;
        
        var deliveryPriceByVolume = (decimal)(volumeCm3 * DeliveryCoefficientSm2);
        return deliveryPriceByVolume;
    }
    
    private static decimal CalculatePriceByWeightV2(DeliveryModel deliveryModel, out int weightKg)
    {
        weightKg = deliveryModel.Goods!
            .Sum(x=>x.Weight/1000) ?? 0;
        

        var deliveryPriceByWeight = (decimal)(weightKg * DeliveryCoefficientByWeightKg) * deliveryModel.Distance;
        return deliveryPriceByWeight;
    }

    private static decimal CalculatePriceByVolumeV2(DeliveryModel deliveryModel, out double volumeM3)
    {
        var volumeCm3 = deliveryModel.Goods!
            .Select(x => x.Height * x.Lenght * x.Wight)
            .Sum();

        volumeM3 = volumeCm3 / 1_000_000.0; 

        var deliveryPriceByVolume = (decimal)(volumeM3 * DeliveryCoefficientM2) * deliveryModel.Distance;
        return deliveryPriceByVolume;
    }
    
    public Cargo[] GetHistoryCargos(int countItems)
    {
        var dbCargos = _storageRepository
            .GetCargos(countItems)
            .ToList();

        var orderedCargoEnumerable = dbCargos
            .OrderByDescending(x => x.DateAt);

        return orderedCargoEnumerable
            .Select(c => new Cargo(c.Volume, c.Weight, c.Price, c.Distance))
            .ToArray();
    }
    
    
    public void DeleteHistoryCargos()
    {
        _storageRepository.DeleteAllCargos();
    }
}
