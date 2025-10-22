using System;

namespace SafeBoda.Core
{
    // Location entity
    public class Location
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Location() { }

        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }

    // Rider entity
    public class Rider
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public Rider() { }

        public Rider(Guid id, string name, string phoneNumber)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }

    // Driver entity
    public class Driver
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string MotoPlateNumber { get; set; }

        public Driver() { }

        public Driver(Guid id, string name, string phoneNumber, string motoPlateNumber)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            MotoPlateNumber = motoPlateNumber;
        }
    }

    // Trip entity
    public class Trip
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid RiderId { get; set; }
        public Rider Rider { get; set; }

        public Guid DriverId { get; set; }
        public Driver Driver { get; set; }

        public Guid StartLocationId { get; set; }
        public Location Start { get; set; }

        public Guid EndLocationId { get; set; }
        public Location End { get; set; }

        public decimal Fare { get; set; }
        public DateTime RequestTime { get; set; }

        public Trip() { }
    }
}