using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Core.Common.Extensions;
using Core.Interfaces;

namespace Core.Models;

public class Client : User, IAdd<Property>
{
    [JsonConstructor]
    public Client(IReadOnlyCollection<Property> param, string email = "", string pass = "") : base(email, pass)
    {
        var inotifyable = param is INotifyCollectionChanged inotify ? inotify : null;

        Properties = (IReadOnlyCollection<Property>)inotifyable ?? new ObservableCollection<Property>(param);
        ((INotifyCollectionChanged)Properties).OnAdd<Property>(item => item.Owner = this);
    }

    public Client(string firstName, string lastName, string email, string pass) : this(Array.Empty<Property>(), email, pass)
    {
        FirstName = firstName ?? throw new ArgumentException("{0} cannot be null", nameof(firstName));
        LastName = lastName ?? throw new ArgumentException("{0} cannot be null", nameof(lastName));
    }

    [Required]
    [MinLength(3)]
    [MaxLength(15)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MinLength(3)]
    [MaxLength(30)]
    public string LastName { get; set; } = string.Empty;

    public IReadOnlyCollection<Property> Properties { get; }

    public void Add(Property prop) => ((IList<Property>)Properties).Add(prop);
}
