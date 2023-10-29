// See https://aka.ms/new-console-template for more information

using ConsoleApp;

var userChoice = "";
do
{
    Console.WriteLine("RSA encryption - RSA, Brute force decryption - BFD, eXit - X?");
    userChoice = Console.ReadLine()?.ToUpper().Trim();
    
    switch (userChoice)
    {
        case "RSA":
            RSA.DoRSA();
            break;
        case "BFD":
            BruteForce.DoBruteForce();
            break;
        case "X":
            break;
        default:
            Console.WriteLine("Bad input!");
            break;
    }
} while (userChoice != "X");