using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Client : User
{
    private Client() : base(null!, null!)
    {
    }

    public Client(string firstName, string lastName, string email, string pass) : base(email, pass)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    [Required]
    [MinLength(3)]
    [MaxLength(15)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string LastName { get; set; } = string.Empty;


    private List<Property> _properties = new List<Property>();
    public IReadOnlyCollection<Property> Properties => _properties;

    public void Add(Property prop)
    {
        prop.Owner = this;
        _properties.Add(prop);
    }
}
