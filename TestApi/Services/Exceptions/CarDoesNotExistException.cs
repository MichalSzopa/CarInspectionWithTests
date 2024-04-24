using System.ComponentModel.DataAnnotations;

namespace TestApi.Services.Exceptions;

public class CarDoesNotExistException : ValidationException
{
}