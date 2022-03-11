using System.Collections;

namespace UI;

public class BadRequest : IEnumerable<(string, string)>
{
    public IDictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();

	public IEnumerator<(string, string)> GetEnumerator()
	{
		foreach (var error in Errors)
		{
			yield return (error.Key, error.Value);
		}
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
