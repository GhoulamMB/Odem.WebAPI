using System.Security.Cryptography;
using System.Text;
namespace Odem.WebAPI.Utils;

public static class Crypto
{
    private static readonly MD5 Md5 = MD5.Create();

    public static string EncryptMd5(string arg)
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

    public static string EncryptBcrypt(string arg)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        return BCrypt.Net.BCrypt.HashPassword(arg, salt);
    }
}