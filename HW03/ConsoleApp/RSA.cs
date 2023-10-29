namespace ConsoleApp;

using System.Numerics;

public class RSA
{
    public static void DoRSA()
    {
            Console.WriteLine("Hello, RSA! You need to understand that if you want to encrypt any long INT the result of multiplication" +
                              "of p and q which you will input further need to be bigger than INT you want to encrypt");
        
            // TODO: find 2 primes...
            long p = 0; // 7919
            long q = 0; // 6841
            
            Console.WriteLine("Insert value of p, it should be a prime number (divisible only by 1 and itself) and not 2");
            do
            {
                p = AssignValue();
                if (!IsPrime(p))
                {
                    p = 0;
                    Console.WriteLine("The number you wrote isn't prime");
                } else if (p == 2)
                {
                    p = 0;
                    Console.WriteLine("The number can't be 2");
                }
                    
            } while (p == 0);
            
            Console.WriteLine("Insert value of q, it should be a prime number (divisible only by 1 and itself), not 2 and not equal to p");
            do
            {
                q = AssignValue();
                if (!IsPrime(q))
                {
                    q = 0;
                    Console.WriteLine("The number you wrote isn't prime");
                } else if (p == q)
                {
                    q = 0;
                    Console.WriteLine("The number you wrote is equal to p");
                } else if (q == 2)
                {
                    q = 0;
                    Console.WriteLine("The number can't be 2");
                }
            } while (q == 0);
        
            var n = p*q;
            var m = (p - 1) * (q - 1);
            
            Console.WriteLine("===================");
            Console.WriteLine($"p: {p}");
            Console.WriteLine($"q: {q}");
            Console.WriteLine($"n: {n}");
            Console.WriteLine($"m: {m}");
            Console.WriteLine("===================");
            
            long e = 0;
            
            for (e = 2; e < m; e++)
            { 
                if (Gcd(e, m) == 1) break;
            }
        
            Console.WriteLine($"e (coprime to the m): {e}");
        
            long d = FindD(m, e);
        
            Console.WriteLine($"d: {d}");
            Console.WriteLine("===================");

            Console.WriteLine($"Private key: d:{d}, m:{m}");
            Console.WriteLine("===================");
            
            Console.WriteLine($"Public key: n: {n}, e: {e}");
            Console.WriteLine("===================");
        
            long plainInput = 0;
            
            Console.Write("Enter a message (positive integer): ");
            do
            {
                if (long.TryParse(Console.ReadLine(), out plainInput) && plainInput > 0)
                {
                    if (plainInput >= n)
                    {
                        Console.WriteLine("Message is too large. Choose a smaller message.");
                        plainInput = 0;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a positive integer.");
                    plainInput = 0;
                }
            } while (plainInput == 0);
            
            
            Console.WriteLine($"Original plain: {plainInput}");
        
            long cipher = ModPow(plainInput, e, n);
            long plainOutput = ModPow(cipher, d, n);

            Console.WriteLine($"Encrypted integer: {cipher}");
            Console.WriteLine($"Plain decrypted integer: {plainOutput}");
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
    
    private static long Gcd(long a, long b)
    {
        if (a == 0)
        {
            return b;
        }
        
        return Gcd(b % a, a);
    }
    
    private static long FindD(long m, long e)
    {
        for (int k = 0; k < long.MaxValue; k++)
        {
            if ((1 + k * m) % e == 0)
            {
                return (1 + k * m) / e;
            }
        }
        
        throw new ApplicationException("D is not found");
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
                if (num <= 0)
                    num = 0;
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