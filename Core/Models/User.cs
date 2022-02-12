using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class User : Identifiable
{
    private User() { }

    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
