using TestApi.Data;
using Microsoft.EntityFrameworkCore;
using TestApi.Repositories;
using TestApi.Services;
using TestApi.Services.Exceptions;

namespace TestProject
{
    public class Tests
    {
        private ApplicationDbContext context;
        private GlobalRepository repository;
        private GlobalService service;

        public Tests()
        {
        }

        [SetUp]
        public async Task Setup()
        {
            context = new ApplicationDbContext();

            await context.Inspections.ExecuteDeleteAsync();
            await context.Cars.ExecuteDeleteAsync();
            await context.SaveChangesAsync();

            repository = new GlobalRepository(context);
            service = new GlobalService(repository);
        }

        [TearDown]
        public async Task Teardown()
        {
            await context.DisposeAsync();
        }

        [Test]
        public async Task ShouldCreateCars()
        {
            // Arrange
            var vin1 = Guid.NewGuid();
            var vin2 = Guid.NewGuid();
            var vin3 = Guid.NewGuid();

            // Act
            await service.AddCar(vin1);
            await service.AddCar(vin2);
            await service.AddCar(vin3);

            // Assert
            var cars = await service.GetAllCars();
            Assert.IsNotNull(cars);
            Assert.IsNotEmpty(cars);
            Assert.AreEqual(3, cars.Count());
            Assert.IsTrue(cars.Any(c => c.VIN == vin1));
        }

        [Test]
        public async Task ShoudThrowCarAlreadyExist()
        {
            // Arrange
            var vin = new Guid();

            await service.AddCar(vin);

            // Act + Assert
            Assert.ThrowsAsync<CarAlreadyExistException>(async () => await service.AddCar(vin));
        }

        [Test]
        public async Task ShouldThrowCarDoesNotExist()
        {
            // Arrange
            var vin = new Guid();
            var mileage = 150000;
            var comments = string.Empty;
            var images = new List<string>() { "a", "b", "c" };

            // Act + Assert
            Assert.ThrowsAsync<CarDoesNotExistException>(async () => await service.InspectCar(vin, mileage, comments, images));
        }

        [Test]
        public async Task ShouldThrowLowerMileage()
        {
            // Arrange
            var vin = new Guid();
            var oldMileage = 150000;
            var newMileage = 140000;
            var comments = string.Empty;
            var images = new List<string>() { "a", "b", "c" };

            await service.AddCar(vin);
            await service.InspectCar(vin, oldMileage, comments, images);

            // Act + Assert
            Assert.ThrowsAsync<LowerMileageException>(async () => await service.InspectCar(vin, newMileage, comments, images));
        }

        [Test]
        public async Task ShouldThrowNotEnoughImages()
        {
            // Arrange
            var vin = new Guid();
            var mileage = 150000;
            var comments = string.Empty;
            var images = new List<string>() { "a", "b" };

            await service.AddCar(vin);

            // Act + Assert
            Assert.ThrowsAsync<NotEnoughImagesException>(async () => await service.InspectCar(vin, mileage, comments, images));
        }
    }
}