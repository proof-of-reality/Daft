namespace Core.Models.Requests;
public class Search : Pagination
{
    public OfferPurpose? Purpose { get; set; }
    public decimal? Value { get; set; }
    public string? Text { get; set; }
}
