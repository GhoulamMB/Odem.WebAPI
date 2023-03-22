namespace Odem.WebAPI.Utils;
using BCrypt.Net;

public static class Crypto
{
    public static string EncryptBcrypt(string arg)
    {
        var salt = BCrypt.GenerateSalt(12);
        return BCrypt.HashPassword(arg, salt);
    }
    
    public static bool CompareBcrypt(string arg, string hash)
    {
        return BCrypt.Verify(arg, hash);
    }
}