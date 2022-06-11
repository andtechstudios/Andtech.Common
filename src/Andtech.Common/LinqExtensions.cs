using System;
using System.Collections.Generic;
using System.Linq;

namespace Andtech.Common
{

	public static class LinqExtensions
	{

		public static bool TryFirst<T>(this IEnumerable<T> enumerable, out T value)
		{
			bool contains = enumerable.Any();
			value = contains ? enumerable.FirstOrDefault() : default;

			return contains;
		}

		public static bool TryFirst<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, out T value)
		{
			bool contains = enumerable.Any(predicate);
			value = contains ? enumerable.FirstOrDefault(predicate) : default;

			return contains;
		}

		public static bool TryLast<T>(this IEnumerable<T> enumerable, out T value)
		{
			bool contains = enumerable.Any();
			value = contains ? enumerable.LastOrDefault() : default;

			return contains;
		}

		public static bool TryLast<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, out T value)
		{
			bool contains = enumerable.Any(predicate);
			value = contains ? enumerable.LastOrDefault(predicate) : default;

			return contains;
		}
	}
}
