using System.Security.Cryptography;
using System.Text;

namespace LinkShorter.BL.Helpers;

public static class HashHelper
{
    private const int ShortUrlLength = 9;

    public static string CreateMd5(string input)
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(input);
        var hash = md5.ComputeHash(inputBytes);
        var sb = new StringBuilder();

        foreach (var t in hash) sb.Append(t.ToString("X2"));

        return sb.ToString().Substring(0, ShortUrlLength);
    }
}