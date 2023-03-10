using System.Security.Cryptography;
using System.Text;

namespace Odem.WebAPI.Utils;

public static class Crypto
{
    private static readonly MD5 Md5 = MD5.Create();

    public static string Encrypt(string arg)
    {
        var bytes = Encoding.UTF8.GetBytes(arg);
        var hashbytes = Md5.ComputeHash(bytes);

        var sb = new StringBuilder();
        foreach (var b in hashbytes)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }
}