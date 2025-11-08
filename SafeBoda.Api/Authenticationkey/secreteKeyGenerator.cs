using System;
using System.Security.Cryptography;

class P
{
    static void Main()
    {
        var key = GenerateJwtSecret(32); // 32 bytes = 256 bits
        Console.WriteLine("JWT Secret Key: " + key);
    }

    static string GenerateJwtSecret(int size)
    {
        // Generate random bytes
        var bytes = new byte[size];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }

        // Convert to Base64 string
        return Convert.ToBase64String(bytes);
    }
}