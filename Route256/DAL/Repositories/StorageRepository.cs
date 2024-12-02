using Route256.BLL.DeliveryPrice.Models;

namespace Route256.DAL.Repositories;

public class StorageRepository : IStorageRepository
{
    private static readonly List<Cargo> CargosStorage = [];
    
    public void SaveCargo(Cargo cargo)
    {
        CargosStorage.Add(cargo);
    }

    public Cargo[] GetCargos(int countItems)
    {
        return CargosStorage.Take(countItems).ToArray();
    }
}