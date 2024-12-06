using Route256.DAL.Entities;

namespace Route256.DAL.Repositories;

public class StorageRepository : IStorageRepository
{
    private static readonly List<CargoDb> CargosStorage = [];
    
    public void SaveCargo(CargoDb cargo)
    {
        CargosStorage.Add(cargo);
    }

    public CargoDb[] GetCargos(int countItems)
    {
        return CargosStorage.Take(countItems).ToArray();
    }

    public void DeleteAllCargos()
    {
        CargosStorage.Clear();
    }
}