namespace ConsoleApp;

using System.Text;

public static class Cesar
{
    // encrypt
    public static string DoCesarEncrypt(string plaintext, int cypherKey)
    {
        // input: utf8 text and check
        var plainText = plaintext;
        
        // input to cesar: shift amount
        var shiftAmount = GetCesarShiftAmount(cypherKey);
        
        // input to cesar: utf8 bytes
        var textBytes = Encoding.UTF8.GetBytes(plainText);
        
        // do cesar
        for (int i = 0; i < textBytes.Count(); i++)
        {
            textBytes[i] =(byte) ((textBytes[i] + shiftAmount) % 256);
        }

        return System.Convert.ToBase64String(textBytes);
        
        
    }

    // public static void DoCesarDecrypt()
    // {
    //     // decrypt
    //     // input: base64 text and check
    //     var encryptedText = "";
    //     do
    //     {
    //         Console.Write("Base64 text: ");
    //         encryptedText = Console.ReadLine();
    //
    //         if (!IsValidBase64(encryptedText) || string.IsNullOrWhiteSpace(encryptedText))
    //         {
    //             encryptedText = "";
    //             Console.WriteLine("Your Base64 input is invalid!");
    //         }
    //     } while (encryptedText == "");
    //     
    //     // decode base64 to bytes
    //     var encryptedBytes = System.Convert.FromBase64String(encryptedText);
    //     
    //     // input shift
    //     var shiftAmount = Cesar.GetCesarShiftAmount();
    //     
    //     // output encrypted bytes (with shift)
    //     /* foreach (var encryptedByte in encryptedBytes)
    //     {
    //         Console.Write(encryptedByte + " ");
    //     }
    //     Console.WriteLine(); */
    //     
    //     // shift
    //     for (var i = 0; i < encryptedBytes.Length; i++)
    //     {
    //         // reverse of (byte) ((textBytes[i] + shiftAmount) % 256)
    //         encryptedBytes[i] = (byte) (((encryptedBytes[i] - shiftAmount) + 256) % 256);
    //     }
    //     
    //     // output decrypted bytes (without shift)
    //     /* foreach (var encryptedByte in encryptedBytes)
    //     {
    //         Console.Write(encryptedByte + " ");
    //     }
    //     Console.WriteLine(); */
    //     
    //     // convert bytes to utf8 and output
    //     Console.WriteLine(Encoding.UTF8.GetString(encryptedBytes));
    // }
    
    private static byte GetCesarShiftAmount(int cypherKey)
    {
        var shiftAmount = cypherKey;
        do
        {
            shiftAmount %= 256;
            if (shiftAmount < 0)
            {
                shiftAmount += 256;
            }
            
            if (shiftAmount == 0)
            {
                Console.WriteLine("0 is not valid shift amount!");
            }
        
        } while (shiftAmount == 0);
    
        return (byte)shiftAmount;
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