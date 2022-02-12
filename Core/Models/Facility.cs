using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Facility : Identifiable
{
    private Facility()
    {
    }

    public Facility(string text, Property property)
    {
        Text = text;
        Property = property;
        PropertyId = property.Id;
    }

    [Required]
    public string Text { get; set; } = string.Empty;


    [ForeignKey(nameof(Property))]
    public long PropertyId { get; set; }

    public Property Property { get; set; } = null!;
}
