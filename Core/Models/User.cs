using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class User : Identifiable
{
    protected User() { }

    public User(string email, string password)
    {
        Email = email ?? throw new ArgumentException("{0} cannot be null", nameof(email));
        Password = password ?? throw new ArgumentException("{0} cannot be null", nameof(password));
    }

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(6), DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
