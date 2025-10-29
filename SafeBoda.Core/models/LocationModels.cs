namespace SafeBoda.Core;

public class Location
{
    public Guid Id = Guid.NewGuid();
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public Location(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
