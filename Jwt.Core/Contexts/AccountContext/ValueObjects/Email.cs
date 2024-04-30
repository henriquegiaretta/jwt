using System.Text.RegularExpressions;
using Jwt.Core.Contexts.SharedContext.Extensions;
using Jwt.Core.Contexts.SharedContext.ValueObjects;

namespace Jwt.Core.Contexts.AccountContext.ValueObjects;

public partial class Email : ValueObject
{
    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    protected Email()
    { }
    
    public Email(string address)
    {
        if (string.IsNullOrEmpty(address) || address.Length < 5 || !EmailRegex().IsMatch(address))
            throw new Exception($"Email {address} é invál   ido");
        
        Address = address.Trim().ToLower();
    }

    public string Address { get; } = string.Empty;
    public string Hash => Address.ToBase64();
    public Verification Verification { get; private set; } = new();

    public void ResendVerification()
        => new Verification();
    
    public static implicit operator string(Email email)
        => email.ToString();
    public static implicit operator Email(string address)
        => new Email(address);

    public override string ToString()
        => Address;
    
    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();
}