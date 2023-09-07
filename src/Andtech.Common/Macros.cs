using Andtech.Common.Text.SentenceExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Andtech.Common
{

	public static class Macros
	{

		/// <summary>
		/// Split the query based on a standard format.
		/// </summary>
		/// <param name="text">The query string.</param>
		/// <returns>The tokens of the query.</returns>
		public static IEnumerable<string> Tokenize(string text)
		{
			text = text?.Trim();

			if (string.IsNullOrEmpty(text))
			{
				return Enumerable.Empty<string>();
			}

			return Regex.Split(text, @"\s+");
		}

		public static bool IsAnySuccess(Text.SentenceExpressions.Match termCollection)
		{
			var success = true;
			success &= termCollection.Success;

			return success;
		}

		public static bool IsStrictSuccess(Text.SentenceExpressions.Match termCollection)
		{
			var success = true;
			success &= termCollection.Success;
			success &= termCollection.Any(x => !x.Word.IsParenthesized && x.IsMatch);
			if (termCollection.Any(x => x.Word.IsParenthesized))
			{
				success &= termCollection.Any(x => x.Word.IsParenthesized && x.IsMatch);
			}

			return success;
		}
	}
}
