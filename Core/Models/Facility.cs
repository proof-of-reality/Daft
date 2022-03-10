using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Models;

public class Facility : Identifiable
{
    private Facility()
    {
    }

    public Facility(string text = "") => _text = text;

    [Required]
    public string Text { get => _text; set => _text = value ?? throw new ArgumentException("{0} cannot be null", nameof(value)); }
    private string _text;

    [ForeignKey(nameof(Property))]
    public long PropertyId { get; set; }


    private Property? _prop;
    public Property Property
    {
        get => _prop; set
        {
            _prop = value ?? throw new ArgumentNullException(nameof(Property));
            PropertyId = value.Id;
        }
    }
}
