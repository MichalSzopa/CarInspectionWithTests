namespace TestApi.Models;

public class Car
{
    public int Id { get; set; }

    public Guid VIN { get; set; }

    public virtual IEnumerable<Inspection> Inspections { get; set; }
}