public static class ComparerHelper
{
	public static bool Equals<T>(
		T? x,
		T? y)
			where T : IEquatable<T?> =>
				x is null ? y is null : x.Equals(y);

	public static bool Equals(
		string? x,
		string? y) =>
			x is null ? y is null : x.Equals(y, StringComparison.Ordinal);

	public static bool Equals<T>(
		IReadOnlyCollection<T?>? x,
		IReadOnlyCollection<T?>? y)
			where T : IEquatable<T>
	{
		if (x is null)
			return y is null;
		if (y is null)
			return false;
		if (x.Count != y.Count)
			return false;

		var xEnumerator = x.GetEnumerator();
		var yEnumerator = y.GetEnumerator();

		while (xEnumerator.MoveNext() && yEnumerator.MoveNext())
		{
			var xCurrent = xEnumerator.Current;
			var yCurrent = yEnumerator.Current;

			if (xCurrent is null)
				return yCurrent is null;
			if (!xCurrent.Equals(yCurrent))
				return false;
		}

		return true;
	}

	public static bool Equals<TKey, TValue>(
		IReadOnlyDictionary<TKey, TValue>? x,
		IReadOnlyDictionary<TKey, TValue>? y)
			where TKey : IEquatable<TKey>
			where TValue : IEquatable<TValue?>
	{
		if (x is null)
			return y is null;
		if (y is null)
			return false;
		if (x.Count != y.Count)
			return false;

		foreach (var xPair in x)
		{
			if (y.TryGetValue(xPair.Key, out var yValue))
				return false;
			if (!xPair.Value.Equals(yValue))
				return false;
		}

		return true;
	}
}
