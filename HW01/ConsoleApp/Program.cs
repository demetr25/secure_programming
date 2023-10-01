// See https://aka.ms/new-console-template for more information

using System.Runtime.ConstrainedExecution;
using System.Text;
using ConsoleApp;

// dialog window to choose activity
var userChoice = "";
do
{
    Console.WriteLine("Cesar Encrypt - CE, Cesar Decrypt - CD, Vigenere Encrypt - VE, Vigenere Decrypt - VD, eXit - X?");
    userChoice = Console.ReadLine()?.ToUpper().Trim();
    switch (userChoice)
    {
        case "CE":
            Cesar.DoCesarEncrypt();
            break;
        case "CD":
            Cesar.DoCesarDecrypt();
            break;
        case "VE":
            Vigenere.DoVigenereEncrypt();
            break;
        case "VD":
            Vigenere.DoVigenereDecrypt();
            break; 
        case "X":
            break;
        default:
            Console.WriteLine("Bad input!");
            break;
    }
} while (userChoice != "X");
