using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

public class Property : Identifiable
{
    private Property()
    {
    }

    public Property(string address, OfferPurpose category, PropertyType type, decimal price)
    {
        Address = address;
        OfferPurpose = category;
        Type = type;
        Price = price;
    }

    public string Title => $"{Price} per month";

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    public OfferPurpose OfferPurpose { get; set; }

    [Required]
    public PropertyType Type { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public short BedroomsAvaiable { get; set; }

    [Required]
    public DateTime AvaiableFrom { get; set; }

    [Required]
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

    private readonly List<Photo> _photos = new List<Photo>();
    private readonly List<Facility> _facilities = new List<Facility>();

    public IReadOnlyCollection<Photo> Photos => _photos;
    public IReadOnlyCollection<Facility> Facilities => _facilities;

    public void Add(Photo photo)
    {
        photo.Property = this;
        _photos.Add(photo);
    }

    public void Add(Facility facility) => _facilities.Add(facility);
}
