using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public abstract class Identifiable
{
    [Key, Required]
    public long Id { get; set; }
}
