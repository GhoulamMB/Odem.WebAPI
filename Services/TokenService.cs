using System.Text;
using Odem.WebAPI.Models.response;

namespace Odem.WebAPI.Services;

public class TokenService
{
    private readonly Dictionary<string, string> _tokens;
    private readonly Random _random;

    public TokenService()
    {
        _tokens = new();
        _random = new Random();
    }

    public string RegisterToken(string userId)
    {
        var token = TokenGenerator();
        return _tokens.TryAdd(token, userId) ? token : string.Empty;
    }

    public Task<string> RetrieveClientId(string token)
    {
        if (!_tokens.TryGetValue(token, out var clientId)) return Task.FromResult("");
        return Task.FromResult(clientId);
    }

    public Task<bool> TokenExist(string token)
    {
        return Task.FromResult(_tokens.ContainsKey(token));
    }
    private string TokenGenerator()
    {
        bool lowerCase = true;
        var builder = new StringBuilder();

        // Unicode/ASCII Letters are divided into two blocks
        // (Letters 65–90 / 97–122):
        // The first group containing the uppercase letters and
        // the second group containing the lowercase.

        // char is a single Unicode character
        char offset = lowerCase ? 'a' : 'A';
        const int lettersOffset = 26; // A...Z or a..z: length=26

        for (var i = 0; i < 12; i++)
        {
            var @char = (char)_random.Next(offset, offset + lettersOffset);
            builder.Append(@char);
        }

        return lowerCase ? builder.ToString().ToLower() : builder.ToString();
    }
}