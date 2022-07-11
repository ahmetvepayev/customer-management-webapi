using System.Text;

namespace CustomerManagement.Utility.Extensions;

public static class StringExtensions
{
    public static string GetOnlyDigits(this string input)
    {
        return new String(input.Where(Char.IsDigit).ToArray());
    }

    public static byte[] ToByteArray(this string input)
    {
        return Encoding.UTF8.GetBytes(input);
    }
}