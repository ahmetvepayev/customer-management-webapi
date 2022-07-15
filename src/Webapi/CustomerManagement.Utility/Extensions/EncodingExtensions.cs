using System.Text;

namespace CustomerManagement.Utility.Extensions;

public static class EncodingExtensions
{
    public static byte[] ToByteArrayUTF8(this string input)
    {
        return Encoding.UTF8.GetBytes(input);
    }

    public static string ToStringUTF8(this byte[] input)
    {
        return Encoding.UTF8.GetString(input);
    }

    public static byte[] ToByteArrayFromBase64(this string input)
    {
        return Convert.FromBase64String(input);
    }
}