// See https://aka.ms/new-console-template for more information

using ConsoleApp;

var userChoice = "";
do
{
    Console.WriteLine("Diffie-Hellman encryption - DH, Prime number multiplication result factorization - FAC, eXit - X?");
    userChoice = Console.ReadLine()?.ToUpper().Trim();
    switch (userChoice)
    {
        case "DH":
            DiffieHellman.DoDiffieHellman();
            break;
        case "FAC":
            Factorization.DoFactorization();
            break;
        case "X":
            break;
        default:
            Console.WriteLine("Bad input!");
            break;
    }
} while (userChoice != "X");