using System.ComponentModel.DataAnnotations;

namespace TestApi.Services.Exceptions;

public class CarAlreadyExistException : ValidationException
{
}