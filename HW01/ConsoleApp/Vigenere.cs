﻿using System.Text;

namespace ConsoleApp;

public class Vigenere
{
    public static void DoVigenereEncrypt()
    {
        // input: utf8 text and check
        var plainText = "";
        do
        {
            Console.Write("Text to encrypt: ");
            plainText = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(plainText))
            {
                plainText = "";
            }
            
        } while (plainText == "");
        
        // input: key string
        var origKey = GetVigenereCypherString();
        
        // make key string the same length as plain text
        var finalKey = origKey;
        while (finalKey.Length < plainText.Length)
        {
            finalKey += origKey;
        }
        finalKey = finalKey.Substring(0, plainText.Length);
        
        // check of values
        /* Console.WriteLine(plainText + " + " + origKey + " = " + finalKey);
        Console.WriteLine(plainText.Length + " + " + finalKey.Length); */
        
        // convert plain text to bytes
        var textBytes = Encoding.UTF8.GetBytes(plainText);
        var keyBytes = Encoding.UTF8.GetBytes(finalKey);

        // output bytes before changes
        /* foreach (var textByte in textBytes)
        {
            Console.Write(textByte + " ");
        }
        Console.WriteLine();
        foreach (var keyByte in keyBytes)
        {
            Console.Write(keyByte + " ");
        }
        Console.WriteLine(); */

        // do vigenere
        for (var i = 0; i < textBytes.Count(); i++)
        {
            textBytes[i] = (byte)((textBytes[i] + keyBytes[i]) % 256);
        }
        
        // output resulting bytes
        /* foreach (var textByte in textBytes)
        {
            Console.Write(textByte + " ");
        }
        Console.WriteLine(); */
        
        // convert result bytes to text - base64
        Console.WriteLine("-------------------");
        Console.WriteLine($"Vigenere encrypted text with key string '{origKey}': ");
        Console.WriteLine(System.Convert.ToBase64String(textBytes));
        Console.WriteLine("-------------------");
    }
    
    public static void DoVigenereDecrypt()
    {
        // input Base64 text and check
        var encryptedText = "";
        do
        {
            Console.Write("Base64 text: ");
            encryptedText = Console.ReadLine();

            if (!IsValidBase64(encryptedText) || string.IsNullOrWhiteSpace(encryptedText))
            {
                encryptedText = "";
                Console.WriteLine("Your Base64 input is invalid!");
            }
        } while (encryptedText == "");
        
        // convert Base64 text to bytes (encrypted)
        var encryptedBytes = System.Convert.FromBase64String(encryptedText);

        //input: key string
        var origKey = GetVigenereCypherString();
        
        // make key string the same length as text which is now encrypted
        var finalKey = origKey;
        for (var i = 0; i < encryptedBytes.Count(); i++)
        {
            finalKey += origKey;
        }
        finalKey = finalKey.Substring(0, encryptedBytes.Count());
        
        // check of values
        /* Console.WriteLine("Final key: " + finalKey);
        Console.WriteLine(encryptedBytes.Count() + " + " + finalKey.Length); */
        
        // convert key string to bytes
        var keyBytes = Encoding.UTF8.GetBytes(finalKey);

        // output bytes before decrypt
        /* foreach (var encryptedByte in encryptedBytes)
        {
            Console.Write(encryptedByte + " ");
        }
        Console.WriteLine();
        foreach (var keyByte in keyBytes)
        {
            Console.Write(keyByte + " ");
        }
        Console.WriteLine(); */
        
        // do decrypt
        for (var i = 0; i < encryptedBytes.Count(); i++)
        {
            encryptedBytes[i] = (byte)(((encryptedBytes[i] - keyBytes[i]) + 256) % 256);
        }

        // output decrypted bytes
        /* foreach (var encryptedByte in encryptedBytes)
        {
            Console.Write(encryptedByte + " ");
        }
        Console.WriteLine(); */
        
        // convert decrypted bytes to utf8 and output
        Console.WriteLine("-------------------");
        Console.WriteLine($"Plain text with key string '{origKey}': ");
        Console.WriteLine(Encoding.UTF8.GetString(encryptedBytes));
        Console.WriteLine("-------------------");
    }

    private static string GetVigenereCypherString()
    {
        var cypherString = "";
        do
        {
            Console.WriteLine("Cypher string: ");
            cypherString = Console.ReadLine();
            if (cypherString == "")
            {
                Console.WriteLine("Your have to enter something!");
            }
        } while (cypherString == "");
        return (string)cypherString;
    }
    
    private static bool IsValidBase64(string input)
    {
        try
        {
            // Attempt to decode the Base64 string
            var data = System.Convert.FromBase64String(input);
            return true; // If decoding succeeds, it's valid Base64
        }
        catch (FormatException)
        {
            return false; // Decoding failed; it's not valid Base64
        }
    }
}