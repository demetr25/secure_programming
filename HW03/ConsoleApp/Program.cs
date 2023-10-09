// See https://aka.ms/new-console-template for more information

using System.Numerics;

Console.WriteLine("Hello, RSA!");

// TODO: find 2 primes...
long p = 7919; // 7919
long q = 6841; // 6841

var n = p*q;
var m = (p-1)*(q-1);

Console.WriteLine($"p: {p}");
Console.WriteLine($"q: {q}");
Console.WriteLine($"n: {n}");
Console.WriteLine($"m: {m}");
long e = 0;
for (e = 2; e < m; e++)
{
    if (Gcd(e, m) == 1) break;
}

Console.WriteLine($"Coprime e to the m: {e}");

long d = FindD(m, e);

Console.WriteLine($"d: {d}");

Console.WriteLine($"Private key: {n} {e}");
Console.WriteLine($"Public key: {n} {d}");

long plainInput = 6;

BigInteger cipher = BigInteger.ModPow(plainInput, e, n);
BigInteger plainOutput = BigInteger.ModPow(cipher, d, n);

Console.WriteLine($"plain: {plainOutput}");

return;

long FindD(long m, long e)
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

long Gcd(long a, long b)
{
    if (a == 0)
    {
        return b;
    }

    return Gcd(b % a, a);
}