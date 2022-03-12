using System.ComponentModel.DataAnnotations;

namespace Core.Models.Requests;

public class EmailRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string EmailTo { get; set; } = string.Empty;

    [Required, MinLength(10)]
    public string Text { get; set; } = string.Empty;

    public long PropertyId { get; set; }
}
