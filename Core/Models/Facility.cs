using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Facility : Identifiable
{
    private Facility()
    {
    }

    public Facility(string text)
    {
        Text = text;
    }

    [Required]
    public string Text { get; set; } = string.Empty;


    [ForeignKey(nameof(Property))]
    public long PropertyId { get; set; }

    private Property _prop;
    public Property Property
    {
        get => _prop; set
        {
            _prop = value ?? throw new ArgumentNullException(nameof(Property));
            PropertyId = value.Id;
        }
    }
}
