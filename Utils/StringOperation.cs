using System.Security.Cryptography;
using System.Text;

namespace KpiAlumni.Utils;

public class StringOperation
{
    public static string GenerateMd5(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Convert the byte array to a hexadecimal string
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
    public static string MkRandomCode(int length = 32)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        StringBuilder result = new StringBuilder(length);

        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            byte[] uintBuffer = new byte[sizeof(uint)];

            for (int i = 0; i < length; i++)
            {
                rng.GetBytes(uintBuffer);
                uint num = BitConverter.ToUInt32(uintBuffer, 0);
                result.Append(chars[(int)(num % (uint)chars.Length)]);
            }
        }

        return result.ToString();
    }
    
    public static string MkOtp(int length = 6)
    {
        const string chars = "0123456789";
        StringBuilder result = new StringBuilder(length);

        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            byte[] uintBuffer = new byte[sizeof(uint)];

            for (int i = 0; i < length; i++)
            {
                rng.GetBytes(uintBuffer);
                uint num = BitConverter.ToUInt32(uintBuffer, 0);
                result.Append(chars[(int)(num % (uint)chars.Length)]);
            }
        }

        return result.ToString();
    }
    
    public static string MkOtp2(int length = 6)
    {
        if (length <= 0) throw new ArgumentException("Length must be a positive integer");

        StringBuilder result = new StringBuilder(length);

        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            byte[] buffer = new byte[sizeof(uint)];

            for (int i = 0; i < length; i++)
            {
                rng.GetBytes(buffer);
                uint num = BitConverter.ToUInt32(buffer, 0);

                // Get a digit between 0 and 9
                result.Append((num % 10).ToString());
            }
        }

        return result.ToString();
    }
    
    public static string Base64Encode(string? inpString)
    {
        try
        {
            byte[] bytes = Encoding.UTF8.GetBytes(inpString ?? "");
            return Convert.ToBase64String(bytes);
        }
        catch
        {
            return "";
        }
    }
    
    public static string Base64Decode(string? base64EncodedData)
    {
        try
        {
            byte[] bytes = Convert.FromBase64String(base64EncodedData ?? "");
            return Encoding.UTF8.GetString(bytes);
        }
        catch
        {
            return "";
        }
    }
}