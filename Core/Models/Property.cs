using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
    [JsonConstructor]
    public Property()
    {
        _photos = new();
        _facilities = new();
        _photos.CollectionChanged += (s, e) =>
        {
            PhotosChanged?.Invoke(this, e);
            if (e.Action != NotifyCollectionChangedAction.Add) return;
            foreach (Photo item in e.NewItems)
            {
                item.Property = this;
            }
        };

        _facilities.CollectionChanged += (s, e) =>
        {
            FacilitiesChanged?.Invoke(this, e);
            if (e.Action != NotifyCollectionChangedAction.Add) return;
            foreach (Facility item in e.NewItems)
            {
                item.Property = this;
            }
        };
        Owner = new Client("", "", "", "");
    }

    public Property(string address, OfferPurpose category, PropertyType type, decimal price) : this()
    {
        Address = address;
        OfferPurpose = category;
        Type = type;
        Price = price;
    }

    public event NotifyCollectionChangedEventHandler? FacilitiesChanged;
    public event NotifyCollectionChangedEventHandler? PhotosChanged;

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    public OfferPurpose OfferPurpose { get; set; }

    [Required]
    public PropertyType Type { get; set; }

    [Required]
    [Range(50, double.MaxValue)]
    public decimal Price { get; set; } = 51;

    [Required]
    [Range(1, double.MaxValue)]
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

    [ForeignKey(nameof(Owner))]
    public long OwnerId { get; set; }

    public Client Owner { get; set; }

    [JsonPropertyName(nameof(Photos))]
    private ObservableCollection<Photo> _photos;

    [JsonPropertyName(nameof(Facilities))]
    private ObservableCollection<Facility> _facilities;

    public IReadOnlyCollection<Photo> Photos => _photos;

    public IReadOnlyCollection<Facility> Facilities => _facilities;

    public void Add(Photo photo) => _photos.Add(photo);
    public void Remove(Photo photo) => _photos.Remove(photo);

    public void Add(Facility facility) => _facilities.Add(facility);
    public void Remove(Facility facility) => _facilities.Remove(facility);
}
