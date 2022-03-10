using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Common.Extensions;
using Core.Interfaces;

namespace Core.Models;

public enum OfferPurpose
{
    Sell,
    Rent,
    Share,
}

public enum PropertyType
{
    House,
    Flat,
    Appartment
}

public class Property : Identifiable, IAdd<Photo>, IAdd<Facility>
{
    private Property() : this(Client.System)
    {
    }

    public Property(Client owner)
    {
        Owner = owner;
        ((INotifyCollectionChanged)Photos).OnAdd<Photo>(item => item.Property = this);
        ((INotifyCollectionChanged)Facilities).OnAdd<Facility>(item => item.Property = this);
    }

    public Property(string address, OfferPurpose category, PropertyType type, decimal price) : this(new Client())
    {
        Type = type;
        Price = price;
        Address = address;
        OfferPurpose = category;
    }

    public event NotifyCollectionChangedEventHandler? PhotosChanged
    {
        add => ((INotifyCollectionChanged)Photos).CollectionChanged += value;
        remove => ((INotifyCollectionChanged)Photos).CollectionChanged -= value;
    }

    public event NotifyCollectionChangedEventHandler? FacilitiesChanged
    {
        add => ((INotifyCollectionChanged)Facilities).CollectionChanged += value;
        remove => ((INotifyCollectionChanged)Facilities).CollectionChanged += value;
    }

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    public OfferPurpose OfferPurpose { get; set; }

    [Required]
    public PropertyType Type { get; set; }

    [Required, Range(50, double.MaxValue)]
    public decimal Price { get; set; }

    [Required, Range(1, short.MaxValue)]
    public short BedroomsAvaiable { get; set; }

    [Required]
    public DateTime AvaiableFrom { get; set; }

    public DateTime? AvaiableUntil { get; set; }

    [Required]
    public bool OwnerOccupied { get; set; }

    /// <summary>
    /// Male =1, Female =0, Both = null
    /// </summary>
    public bool? Preference { get; set; }

    [Required, ForeignKey(nameof(Owner))]
    public long OwnerId { get; set; }

    public Client Owner { get; set; }

    [MinLength(1)]
    public IReadOnlyCollection<Photo> Photos { get; } = new ObservableCollection<Photo>();

    public IReadOnlyCollection<Facility> Facilities { get; } = new ObservableCollection<Facility>();

    public void Add(Photo photo) => ((IList<Photo>)Photos).Add(photo);
    public void Remove(Photo photo) => ((IList<Photo>)Photos).Remove(photo);

    public void Add(Facility facility) => ((IList<Facility>)Facilities).Add(facility);
    public void Remove(Facility facility) => ((IList<Facility>)Facilities).Remove(facility);
}
