using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using Core.Common.Extensions;
using Core.Interfaces;

namespace Core.Models;

public class Client : User, IAdd<Property>
{
    public static readonly Client System = new Client();

    protected Client() : base()
    {
    }

    public Client(string email = "", string pass = "") : base(email, pass)
    {
        ((INotifyCollectionChanged)Properties).OnAdd<Property>(item => item.Owner = this);
    }

    public Client(string firstName, string lastName, string email, string pass) : this(email, pass)
    {
        FirstName = firstName ?? throw new ArgumentException("{0} cannot be null", nameof(firstName));
        LastName = lastName ?? throw new ArgumentException("{0} cannot be null", nameof(lastName));
    }

    [Required, MinLength(3), MaxLength(15)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MinLength(3), MaxLength(30)]
    public string LastName { get; set; } = string.Empty;

    public IReadOnlyCollection<Property> Properties { get; } = new ObservableCollection<Property>();

    public void Add(Property prop) => ((IList<Property>)Properties).Add(prop);
}
