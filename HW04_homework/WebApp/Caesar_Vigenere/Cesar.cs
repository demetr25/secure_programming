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
}