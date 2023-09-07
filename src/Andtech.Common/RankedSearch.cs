using System.Collections.Generic;
using System.Linq;
using Andtech.Common.Text.SentenceExpressions;

namespace Andtech.Common
{

	public class RankedSearch<T> where T : ISearchable
	{
		private string query;

		public RankedSearch(string query)
		{
			this.query = query;
		}

		public bool Search(IEnumerable<T> options, out T item)
		{
			var scores = options.Select(ToScore);

			foreach (var score in scores)
			{
				Log.WriteLine(score.Key, Verbosity.silly);
				Log.WriteLine($"{score.BarePartialMatchCount}-{score.BareCount - score.BarePartialMatchCount} ({score.MetaPartialMatchCount}-{score.MetaCount - score.MetaPartialMatchCount})", Verbosity.silly);
				Log.WriteLine($"{score.BareExactMatchCount}-{score.BareCount - score.BareExactMatchCount} ({score.MetaExactMatchCount}-{score.MetaCount - score.MetaExactMatchCount})", Verbosity.silly);
			}

			var candidates = scores.Where(x => x.BarePartialMatchCount > 0);
			if (!candidates.Any())
			{
				item = default;
				return false;
			}

			candidates = candidates
				.OrderByDescending(x => x.MetaPartialMatchCount > 0)
				.ThenByDescending(x => x.BareExactMatchCount)
				.ThenByDescending(x => x.MetaExactMatchCount)
				.ThenByDescending(x => x.BarePartialMatchCount)
				.ThenByDescending(x => x.MetaPartialMatchCount)
				.ThenBy(x => x.BareCount - x.BareExactMatchCount)
				.ThenBy(x => x.MetaCount - x.MetaExactMatchCount)
				.ThenBy(x => x.BareCount - x.BarePartialMatchCount)
				.ThenBy(x => x.MetaCount - x.MetaPartialMatchCount);

			item = candidates.First().Key;
			return true;
		}

		internal Score<T> ToScore(T option)
		{
			var sentex = new Sentex(option.Text);
			var pairs = sentex.Pairs(query, out var success);

			return new Score<T>()
			{
				Key = option,
				BareCount = pairs.Where(x => !x.Word.IsParenthesized).Count(),
				MetaCount = pairs.Where(x => x.Word.IsParenthesized).Count(),
				BarePartialMatchCount = pairs.Where(x => !x.Word.IsParenthesized).Count(x => x.IsMatch),
				BareExactMatchCount = pairs.Where(x => !x.Word.IsParenthesized).Count(x => x.IsExactMatch),
				MetaPartialMatchCount = pairs.Where(x => x.Word.IsParenthesized).Count(x => x.IsMatch),
				MetaExactMatchCount = pairs.Where(x => x.Word.IsParenthesized).Count(x => x.IsExactMatch),
			};
		}

		internal struct Score<T>
		{
			public T Key { get; set; }
			public int BareCount { get; set; }
			public int BarePartialMatchCount { get; set; }
			public int BareExactMatchCount { get; set; }
			public int MetaCount { get; set; }
			public int MetaPartialMatchCount { get; set; }
			public int MetaExactMatchCount { get; set; }
		}
	}
}
