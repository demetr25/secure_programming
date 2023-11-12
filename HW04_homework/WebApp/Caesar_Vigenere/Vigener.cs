using System.Text;

namespace ConsoleApp;

public class Vigener
{
    public static string DoVigenerEncrypt(string plaintext, string cypherKey)
    {
        // input: utf8 text and check
        var plainText = plaintext;
        
        // input: key string
        var origKey = GetVigenerCypherString(cypherKey);
        
        // make key string the same length as plain text
        var finalKey = origKey;
        while (finalKey.Length < plainText.Length)
        {
            finalKey += origKey;
        }
        finalKey = finalKey.Substring(0, plainText.Length);
        
        // convert plain text to bytes
        var textBytes = Encoding.UTF8.GetBytes(plainText);
        var keyBytes = Encoding.UTF8.GetBytes(finalKey);

        // do vigenere
        for (var i = 0; i < textBytes.Count(); i++)
        {
            textBytes[i] = (byte)((textBytes[i] + keyBytes[i]) % 256);
        }

        return System.Convert.ToBase64String(textBytes);
    }

    private static string GetVigenerCypherString(string cypherKey)
    {
        var cypherString = cypherKey;
        do
        {
            if (cypherString == "")
            {
                Console.WriteLine("Your have to enter something!");
            }
        } while (cypherString == "");
        return cypherString;
    }
}