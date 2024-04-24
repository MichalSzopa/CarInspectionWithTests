using Microsoft.AspNetCore.Mvc;
using TestApi.Models;
using TestApi.Services;

namespace TestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GlobalController(IGlobalService service) : ControllerBase
{
    [HttpGet("GetAllCars")]
    public async Task<IEnumerable<Car>> GetAllCars()
    {
        return await service.GetAllCars();
    }

    [HttpPost("AddCar")]
    public async Task AddCar(Guid vin)
    {
        await service.AddCar(vin);
    }

    [HttpGet("GetInspectionForCar")]
    public async Task<Inspection> GetInspectionForCar(Guid vin)
    {
        var inspection = await service.GetPreviousInspection(vin);

        if (inspection == null)
        {
            return null;
        }

        inspection.Car = null;

        return inspection;
    }

    [HttpPost("InspectCar")]
    public async Task InspectCar(Guid vin, int mileage, string comments)
    {
        await service.InspectCar(vin, mileage, comments, new List<string> { "a", "b", "c" });
    }
}