using System.Linq;
using System.Text.RegularExpressions;

namespace Andtech.Common.Text.SentenceExpressions
{

	/// <summary>
	/// Sentence expression.
	/// </summary>
	public class Sentex
	{
		class _Term
		{
			public string Text { get; set; }
			public Regex Regex { get; set; }
		}
		public int WordCount => sentence.Words.Count;

		private readonly Sentence sentence;

		public Sentex(string pattern)
		{
			sentence = Sentence.Parse(pattern);
		}

		public Match Match(string pattern)
		{
			var terms = Pairs(pattern, out var success);
			return new Match()
			{
				Pairs = terms.Where(x => x.IsMatch).ToArray(),
				Success = success,
			};
		}

		public bool IsMatch(string pattern)
		{
			Pairs(pattern, out var success);
			return success;
		}

		public Pair[] Pairs(string pattern, out bool success)
		{
			var n = sentence.Words.Count;
			var pairs = new Pair[n];
			for (int i = 0; i < n; i++)
			{
				pairs[i] = new Pair(sentence.Words[i]);
			}

			var otherWords = Macros
				.Tokenize(pattern)
				.ToList();
			var m = otherWords.Count;

			int index = 0;
			bool skippedAny = false;
			int matchCount = 0;

			for (int j = 0; j < m; j++)
			{
				for (int i = index; i < n; i++)
				{
					var isSubstring = Regex.IsMatch(sentence.Words[i].Value, $@"^{otherWords[j]}[^\s]*");
					if (isSubstring)
					{
						pairs[i].IsMatch = true;
						pairs[i].IsExactMatch = sentence.Words[i].Value == otherWords[j];
						index = i + 1;
						matchCount++;
						goto End;
					}
				}
				skippedAny |= true;

			End:
				{ }
			}

			success = matchCount > 0 && matchCount == m && !skippedAny;
			return pairs;
		}
	}
}
