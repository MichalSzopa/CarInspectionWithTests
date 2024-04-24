using TestApi.Models;

namespace TestApi.Repositories;

public interface IGlobalRepository
{
    Task AddCar(Car car);

    Task<IEnumerable<Car>> GetAllCars();

    Task InspectCar(Inspection inspection);

    Task<bool> IsCarAlreadyInDatabase(Guid vin);

    Task<Inspection> GetPreviousInspection(Guid vin);

    Task<Car> GetCarByVin(Guid vin);
}