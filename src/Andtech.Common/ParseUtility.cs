using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Andtech.Common
{

	public static class ParseUtility
	{

		/// <summary>
		/// Splits the input based on standard quoting rules.
		/// </summary>
		/// <param name="input">The string to split.</param>
		/// <returns>A collection of tokens.</returns>
		public static IEnumerable<string> QuotedSplit(string input)
		{
			var regex = new Regex(@"\""(?<match>((\\\"")|[^\""])*)\""|'(?<match>((\\')|[^'])*)'|(?<match>[^\s]+)");
			return
				from match in regex.Matches(input)
				select match.Groups["match"].Value;
		}
	}
}
