using System.ComponentModel.DataAnnotations;

namespace UI.Pages;

class ClientDTO : Core.Models.Client
{

    public ClientDTO() : this(string.Empty, string.Empty, string.Empty, string.Empty)
    {
    }

    public ClientDTO(string firstName, string lastName, string email, string pass) : base(firstName, lastName, email, pass)
    {
    }

    [Compare("Password", ErrorMessage = "Password and confirm password do not match.")]
    public string Confirmation { get; set; }
}
