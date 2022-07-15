namespace CustomerManagement.Utility.Extensions;

public static class StringExtensions
{
    public static string GetOnlyDigits(this string input)
    {
        return new String(input.Where(Char.IsDigit).ToArray());
    }
}