using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Core.Models;

public  class Client : User, IAdd<Property>
{
    private Client() : base(null!, null!)
    {
    }

    public Client(string firstName, string lastName, string email, string pass) : base(email, pass)
    {
        FirstName = firstName ?? throw new ArgumentException("{0} cannot be null", nameof(firstName));
        LastName = lastName ?? throw new ArgumentException("{0} cannot be null", nameof(lastName));

        _properties.CollectionChanged += (s, e) =>
        {
            if (e.Action != System.Collections.Specialized.NotifyCollectionChangedAction.Add) return;

            foreach (Property item in e.NewItems) item.Owner = this;
        };
    }

    [Required]
    [MinLength(3)]
    [MaxLength(15)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string LastName { get; set; } = string.Empty;

    private ObservableCollection<Property> _properties = new();
    public IReadOnlyCollection<Property> Properties => _properties;

    public void Add(Property prop) => _properties.Add(prop);
}
