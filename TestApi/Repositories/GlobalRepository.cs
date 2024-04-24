using TestApi.Data;
using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Repositories;

public class GlobalRepository(ApplicationDbContext context) : IGlobalRepository
{
    public async Task AddCar(Car car)
    {
        await context.Cars.AddAsync(car);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Car>> GetAllCars()
    {
        return await context.Cars.ToListAsync();
    }

    public async Task InspectCar(Inspection inspection)
    {
        await context.Inspections.AddAsync(inspection);
        await context.SaveChangesAsync();
    }

    public async Task<bool> IsCarAlreadyInDatabase(Guid vin)
    {
        var carExists = await context.Cars.AnyAsync(c => c.VIN == vin);
        return carExists;
    }

    public async Task<Inspection> GetPreviousInspection(Guid vin)
    {
        var car = await context.Cars.Where(c => c.VIN == vin).Include(c => c.Inspections).FirstOrDefaultAsync();
        if (car == null)
        {
            return null;
        }

        return car.Inspections.OrderByDescending(i => i.InspectionDate).FirstOrDefault();
    }

    public async Task<Car> GetCarByVin(Guid vin)
    {
        return await context.Cars.Where(c => c.VIN == vin).Include(c => c.Inspections).FirstOrDefaultAsync();
    }
}