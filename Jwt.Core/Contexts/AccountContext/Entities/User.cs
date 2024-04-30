using Jwt.Core.Contexts.AccountContext.ValueObjects;
using Jwt.Core.Contexts.SharedContext.Entities;

namespace Jwt.Core.Contexts.AccountContext.Entities;

public class User : Entity
{
    protected User()
    { }
    
    public User(string name, Email email, Password password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
    
    public User(string email, string? password = null)
    {
        Email = email;
        Password = new Password(password);
    }
    
    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public Password Password { get; set; } = null!;
    public string Image { get; set; } = string.Empty;
    public List<Role> Roles { get; set; } = new();
    
    
    public void UpdatePassword(string plainTextPassword, string code)
    {
        if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código de restauração inválido");

        var password = new Password(plainTextPassword);
        Password = password;
    }

    public void UpdateEmail(Email email)
        => Email = email;

    public void ChangePassword(string plainTextPassword)
    {
        var password = new Password(plainTextPassword);
        Password = password;
    }
}