using System.Numerics;
using System.Security.Cryptography;

namespace ConsoleApp;

public class Factorization
{
    public static void DoFactorization()
    {
        Console.WriteLine("Hello, Factorization!");

        long prime1 = GenerateRandomPrime();
        long prime2 = GenerateRandomPrime();

        long product = prime1 * prime2;

        Console.WriteLine("Prime 1: " + prime1);
        Console.WriteLine("Prime 2: " + prime2);
        Console.WriteLine("Product: " + product);

        long factor1 = 0;
        long factor2 = 0;
        
        for (long i = 2; i <= product; i++)
        {
            while (product % i == 0 && IsPrime(i))
            {
                if (factor1 == 0)
                {
                    factor1 = i;
                }
                else if (factor2 == 0)
                {
                    factor2 = i;
                    break;
                }

                product /= i;
            }
        }

        Console.WriteLine("Prime Factors: " + factor1 + " and " + factor2);
    }

    private static long GenerateRandomPrime()
    {
        RandomNumberGenerator rng = RandomNumberGenerator.Create();
        byte[] bytes = new byte[8];
        long randomPrime = 0;

        while (!IsPrime(randomPrime))
        {
            rng.GetBytes(bytes);
            randomPrime = BitConverter.ToInt64(bytes, 0) & 0x7FFFFFFFFFFFFFFF; // Ensure positive value
        }

        return randomPrime;
    }
    
    private static bool IsPrime(long number)
    {
        if (number <= 1) return false;
        if (number <= 3) return true;
        if (number % 2 == 0) return false;
        for (long i = 3; i * i <= number; i += 2)
        {
            if (number % i == 0)
                return false;
        }
        return true;
    }
}