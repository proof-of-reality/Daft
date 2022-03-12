namespace Core.Models.Requests;

public class Pagination
{
    public int From { get; set; }
    public virtual int Quantity { get; set; } = 10;

    public static implicit operator Pagination((int, int) p) => new() { From = p.Item1, Quantity = p.Item2 };
}
