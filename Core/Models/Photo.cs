using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Models;

public class Photo : Identifiable
{
    [JsonConstructor]
	public Photo()
	{
	}

    public Photo(string path)
    {
        Data = File.ReadAllBytes(path);
        Format = new FileInfo(path).Extension;
    }

    public Photo(byte[] data, string format)
    {
        Data = data;
        Format = format;
    }

    [Required]
    [MinLength(3)]
    [MaxLength(10)]
    public string Format { get; private set; } = string.Empty;

    [Required]
    [JsonInclude]
    [DataType(DataType.Upload)]
    public byte[] Data { get; private set; } = Array.Empty<byte>();


    [ForeignKey(nameof(Property))]
    public long PropertyId { get; private set; }


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

    /// <summary>
    /// Returns the base64 representation of the byte array of this photo.
    /// </summary>
    /// <param name="p"></param>
    public static explicit operator string(Photo p) => Convert.ToBase64String(p.Data);
}
