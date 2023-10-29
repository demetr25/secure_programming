namespace ConsoleApp;

public class DiffieHellman
{
    public static void DoDiffieHellman()
    {
        Console.WriteLine("Hello, Diffie-Hellman!");

        string str = "";
        
        long p = 0;
        long g = 0;

        long a = 0;
        long b = 0;

        Console.WriteLine("Insert value of p, it should be a prime number (divisible only by 1 and itself)");
        do
        {
            p = AssignValue();
            if (!IsPrime(p))
            {
                p = 0;
            }
        } while (p == 0);
        
        Console.WriteLine("Insert value of g, it should be a base number (1-9)");
        do
        {
            g = AssignValue();
            if (g <= 0 || g > 9)
            {
                g = 0;
            }
        } while (g == 0);
        
        Console.WriteLine("Insert value of a (positive long)");
        a = AssignValue();
        
        Console.WriteLine("Insert value of b (positive long)");
        b = AssignValue();
        
        long xSec = Pow(g, a)%p;
        long ySec = Pow(g, b)%p;

        Console.WriteLine($"{xSec}, {ySec}");

        long xShar = Pow(ySec, a) % p;
        long yShar = Pow(xSec, b) % p;

        if (xShar == yShar)
        {
            Console.WriteLine($"true -> {xShar}");
        }
        else
        {
            Console.WriteLine($"false -> {xShar} + {yShar}");
        }
    }
    
    private static long Pow(long num, long pow)
    {
        long result = 1;
        for (int i = 1; i <= pow; ++i)
        {
            result *= num;
        }

        return result;
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