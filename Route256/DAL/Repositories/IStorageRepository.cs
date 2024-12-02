using Route256.BLL.DeliveryPrice.Models;

namespace Route256.DAL.Repositories;

public interface IStorageRepository
{
    public void SaveCargo(Cargo cargo);
    
    public Cargo[] GetCargos(int countItems);
}