namespace TestApi.Models;

public class Inspection
{
    public int Id { get; set; }

    public int CarId { get; set; }

    public DateTime InspectionDate { get; set; }

    public string Comments { get; set; }

    public List<string> Images { get; set; }

    public int Mileage { get; set; }

    public virtual Car Car { get; set; }
}