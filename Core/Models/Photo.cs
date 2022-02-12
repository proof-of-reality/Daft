using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Photo : Identifiable
{
    [Required]
    [MinLength(3)]
    [MaxLength(10)]
    public string Format { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Upload)]
    public byte[] Data { get; set; } = Array.Empty<byte>();


    [ForeignKey(nameof(Property))]
    public long PropertyId { get; set; }

    public Property Property { get; set; } = null!;
}
