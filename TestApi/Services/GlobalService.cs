using TestApi.Models;
using TestApi.Repositories;
using TestApi.Services.Exceptions;

namespace TestApi.Services;

public class GlobalService(IGlobalRepository globalRepository) : IGlobalService
{
    public async Task AddCar(Guid vin)
    {
        if (await globalRepository.IsCarAlreadyInDatabase(vin))
        {
            throw new CarAlreadyExistException();
        }

        var car = new Car()
        {
            VIN = vin,
        };

        await globalRepository.AddCar(car);
    }

    public async Task<IEnumerable<Car>> GetAllCars()
    {
        return await globalRepository.GetAllCars();
    }

    public async Task<Inspection> GetPreviousInspection(Guid vin)
    {
        return await globalRepository.GetPreviousInspection(vin);
    }

    public async Task InspectCar(Guid vin, int mileage, string comments, List<string> images)
    {
        if (images.Count < 3)
        {
            throw new NotEnoughImagesException();
        }

        var car = await globalRepository.GetCarByVin(vin);

        if (car == null)
        {
            throw new CarDoesNotExistException();
        }

        var previousInspection = await globalRepository.GetPreviousInspection(vin);

        if (previousInspection == null)
        {
            await globalRepository.InspectCar(new Inspection()
            {
                CarId = car.Id,
                InspectionDate = DateTime.Now,
                Mileage = mileage,
                Comments = comments,
                Images = images
            });
            return;
        }

        if (previousInspection.Mileage > mileage)
        {
            throw new LowerMileageException();
        }

        await globalRepository.InspectCar(new Inspection()
        {
            CarId = car.Id,
            InspectionDate = DateTime.Now,
            Mileage = mileage,
            Comments = comments,
            Images = images
        });
    }
}