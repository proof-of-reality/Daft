using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Photo : Identifiable
{

    public Photo(string path) : this(File.ReadAllBytes(path))
    {
    }

    public Photo(byte[] data)
    {
        Data = data;
    }

    [Required]
    [MinLength(3)]
    [MaxLength(10)]
    public string Format { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Upload)]
    public byte[] Data { get; set; } = Array.Empty<byte>();


    [ForeignKey(nameof(Property))]
    public long PropertyId { get; set; }


    private Property? _prop;

    public Property Property
    {
        get => _prop!;
        set
        {
            _prop = value ?? throw new ArgumentException("Cannot be null", nameof(Property));
            PropertyId = value.Id;
        }
    }

    public static explicit operator string(Photo p) => Convert.ToBase64String(p.Data);
}
