using System;

namespace SafeBoda.Core;

public class Driver
{
    public Guid Id { get; set; } = new Guid();

    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string MotoPlateNumber { get; set; }

    public Driver(Guid id, string name, string phoneNumber, string motoPlateNumber)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        MotoPlateNumber = motoPlateNumber;
    }
}
