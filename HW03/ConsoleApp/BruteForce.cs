using System.Numerics;

namespace ConsoleApp;

public class BruteForce
{
    public static void DoBruteForce()
    {
        long encryptedMessage = 0;
        long n = 0;
        long e = 0;
        
        Console.WriteLine("Insert encrypted long integer: ");
        encryptedMessage = AssignValue();

        Console.WriteLine("Insert value of n (first part of public key): ");
        n = AssignValue();
        
        Console.WriteLine("Insert value of e (second part of public key): ");
        e = AssignValue();

        long plainInput = BruteForceDecrypt(encryptedMessage, n, e);

        if (plainInput == -1)
        {
            Console.WriteLine("The program is unable to brute force this encrypted message");
        }
        else
        {
            Console.WriteLine($"Plain integer: {plainInput}");
        }
    }
    
    static long BruteForceDecrypt(long encryptedMessage, long n, long e)
    {
        for (long i = 2; i < n; i++)
        {
            if (ModPow(i, e, n) == encryptedMessage)
            {
                return i;
            }
        }
        return -1; // Not found
    }
    
    private static long AssignValue()
    {
        long num = 0;
        do
        {
            Console.Write("Value: ");
            if (long.TryParse(Console.ReadLine(), out long number))
            {
                num = number;
                // if (num <= 0)
                //     num = 0;
            }
            else
            {
                num = 0;
            }
        } while (num == 0);

        return num;
    }
    
    private static long ModPow(long value, long exponent, long modulus)
    {
        if (modulus == 1)
            return 0;
        
        long result = 1;
        value %= modulus;

        while (exponent > 0)
        {
            if (exponent % 2 == 1)
                result = (result * value) % modulus;

            exponent = exponent >> 1;
            value = (value * value) % modulus;
        }

        return result;
    }
}