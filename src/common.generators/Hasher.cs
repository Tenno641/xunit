public class Hasher
{
	int result;

	Hasher(int start) =>
		result = start;

	public static implicit operator int(Hasher hasher) =>
		Guard.ArgumentNotNull(hasher).result;

	public static Hasher Extend(int start) =>
		new(start);

	public static Hasher Start() =>
		new(64827692);

	public Hasher With(object? value)
	{
		result = result * -1521134295 + (value is null ? 0 : value.GetHashCode());
		return this;
	}

	// Prevent this as coming through the collection path
	public Hasher With(string? value) =>
		With((object?)value);

	public int ToInt32() =>
		result;

	public Hasher With<T>(IEnumerable<T> collection)
	{
		if (collection is null)
			With(null);
		else
			foreach (var value in collection)
				With(value);

		return this;
	}

	public Hasher With<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
	{
		if (dictionary is null)
			With(null);
		else
			foreach (var kvp in dictionary)
			{
				With(kvp.Key);
				With(kvp.Value);
			}

		return this;
	}
}
