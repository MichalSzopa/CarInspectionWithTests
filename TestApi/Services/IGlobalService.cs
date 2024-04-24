using TestApi.Models;

namespace TestApi.Services;

public interface IGlobalService
{
    Task AddCar(Guid vin);

    Task<IEnumerable<Car>> GetAllCars();

    Task<Inspection> GetPreviousInspection(Guid vin);

    Task InspectCar(Guid vin, int mileage, string comments, List<string> images);
}