using Route256.BLL.DeliveryPrice.Models;
using Route256.DAL.Entities;

namespace Route256.DAL.Repositories;

public interface IStorageRepository
{
    public void SaveCargo(CargoDb cargo);
    
    public CargoDb[] GetCargos(int countItems);
    public CargoDb[] GetAllCargos();
    public void DeleteAllCargos();
}