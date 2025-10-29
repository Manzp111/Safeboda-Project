using System;

namespace SafeBoda.Core;

public class Rider
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    public Rider(Guid id, string name, string phoneNumber)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
    }
}
